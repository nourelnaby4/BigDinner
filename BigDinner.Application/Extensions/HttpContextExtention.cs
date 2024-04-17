using Microsoft.AspNetCore.Http;

namespace BigDinner.Application.Extensions;

public static class HttpContextExtention
{
    public static IDictionary<string, string> GetRequestQuery(this HttpContext context)
    {
        IDictionary<string, string> queryParams = context.Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString());
        foreach (var queryParam in context.Request.Query)
        {
            queryParams.Add(queryParam.Key, queryParam.Value.ToString());
        }
        return queryParams;
    }
    public static async Task<string> GetRequestBody(this HttpContext context)
    {
        context.Request.EnableBuffering();
        context.Request.Body.Seek(0, SeekOrigin.Begin);
        using (StreamReader stream = new StreamReader(context.Request.Body))
        {
            var body = await stream.ReadToEndAsync();
            return body;
        }
    }
    public static Dictionary<string, string> GetRequestHeader(this HttpContext context)
    {
        var requestHeaders = context.Request.Headers.SelectMany(header => header.Value.Select(value => new { Name = header.Key, Value = value })).ToDictionary(header => header.Name, header => header.Value);
        return requestHeaders;
    }
    public static async Task<Dictionary<string, string>> GetRequestParameters(this HttpContext context)
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

