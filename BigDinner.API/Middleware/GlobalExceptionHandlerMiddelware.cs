using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Text.Json;
using System.Xml;

namespace BigDinner.API.Middleware;

public class GlobalExceptionHandlerMiddelware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddelware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
            //TODO:: cover all validation errors
            switch (error)
            {
                case UnauthorizedAccessException e:
                    // custom application error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;

                    break;


                case FluentValidation.ValidationException e:
                    // custom validation error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case System.ComponentModel.DataAnnotations.ValidationException e:
                    // custom validation error
                    responseModel.Message = error.Message;
                    responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    responseModel.Message = error.Message; ;
                    responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case DbUpdateException e:
                    // can't update error
                    responseModel.Message = e.Message;
                    responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case Exception e:
                    if (e.GetType().ToString() == "ApiException")
                    {
                        responseModel.Message += e.Message;
                        responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    responseModel.Message = e.Message;
                    responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

                default:
                    // unhandled error
                    responseModel.Message = error.Message;

                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = System.Text.Json.JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);


            var requestHeaders = context.Request.Headers.SelectMany(header => header.Value.Select(value => new { Name = header.Key, Value = value })).ToDictionary(header => header.Name, header => header.Value);
            // Serialize the log data object to JSON
            string logDataJson = JsonConvert.SerializeObject(new
            {
                Timestamp = DateTimeOffset.Now,
                RequestPath = context.Request.Path,
                RequestMethod = context.Request.Method,
                RequestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}",
                QueryString = context.Request.QueryString,
                UserAgent = context.Request.Headers["User-Agent"],
                IPAddress = context.Connection.RemoteIpAddress?.ToString(),
                RequestHeaders = requestHeaders,
                User = context.User?.Claims.ToList(),
                RequestParameters = await GetRequestParameters(context),
                ExceptionType = error.GetType().FullName,
                ErrorMessage = error.Message,
                StackTrace = error.StackTrace,
                InnerException = error.InnerException != null ? new
                {
                    Type = error.InnerException.GetType().FullName,
                    Message = error.InnerException.Message,
                    StackTrace = error.InnerException.StackTrace
                } : null,
                Response = result,
            }, Newtonsoft.Json.Formatting.Indented); // Indented formatting for better readability

            // Log the JSON string
            Log.Error(error, "An error occurred while processing request: {@LogData}", logDataJson);
        }
    }
    private async Task<Dictionary<string, string>> GetRequestParameters(HttpContext context)
    {
        var parameters = new Dictionary<string, string>();

        // Fetch query parameters
        foreach (var (key, value) in context.Request.Query)
        {
            parameters[key] = value.ToString();
        }

        // Fetch form data (if any)
        if (context.Request.HasFormContentType)
        {
            foreach (var (key, value) in context.Request.Form)
            {
                parameters[key] = value.ToString();
            }
        }

        // Fetch JSON payload for POST requests (if applicable)
        if (context.Request.Method == "POST" || context.Request.Method == "PUT")
        {
            // Assuming JSON payload for simplicity, adjust as per your needs
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody))
            {
                var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(requestBody);
                foreach (var (key, value) in json)
                {
                    parameters[key] = value;
                }
            }
        }

        return parameters;
    }
}

