using System.Net;
using System.Text.Json;

namespace MaintenanceApi.Middleware;
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/problem+json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            var problem = new
            {
                type = "https://tools.ietf.org/html/rfc7807",
                title = "Bad Request",
                status = response.StatusCode,
                detail = ex.Message,
                instance = context.Request.Path
            };

            await response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}
