using BigDinner.Application.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

namespace BigDinner.API.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await HandleException(context.HttpContext, context.Exception);
            await LogError(context);
        }
        private async Task HandleException(HttpContext context, Exception error)
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
        }

        private async Task LogError(ExceptionContext context)
        {
            var error = context.Exception;

            // Serialize the log data object to JSON
            string logDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Timestamp = DateTimeOffset.Now,
                RequestPath = context.HttpContext.Request.Path,
                RequestMethod = context.HttpContext.Request.Method,
                RequestUrl = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}",
                UserAgent = context.HttpContext.Request.Headers["User-Agent"],
                IPAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                RequestHeaders = context.HttpContext.GetRequestHeader(),
                RequestParameters = await context.HttpContext.GetRequestParameters(),
                RequestQuery = context.HttpContext.GetRequestQuery(),
                RequestQueryString = context.HttpContext.Request.QueryString,
                RequestBody = await context.HttpContext.GetRequestBody(),
                ExceptionType = error.GetType().FullName,
                ErrorMessage = error.Message,
                StackTrace = error.StackTrace,
                InnerException = error.InnerException != null ? new
                {
                    Type = error.InnerException.GetType().FullName,
                    Message = error.InnerException.Message,
                    StackTrace = error.InnerException.StackTrace
                } : null,
                Response = "", // Placeholder for response
            }, Newtonsoft.Json.Formatting.Indented);  // Indented formatting for better readability
            Log.Error(error, "An error occurred while processing request: {@LogData}", logDataJson);
            context.Result = new StatusCodeResult(500); // Internal Server Error
            context.ExceptionHandled = true;
        }
    }
}
