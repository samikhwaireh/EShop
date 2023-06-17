using System.Net;

namespace EShop.API.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var message = error.Message;

            switch (error)
            {
                case ApplicationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An error occurred!";
                    break;
            }

            var errorResponse = new
            {
                Title = response.StatusCode.ToString(),
                Message = message,
                Status = (int)response.StatusCode,
                Instance = $"urn:{Dns.GetHostName()}",
            };

            await response.WriteAsJsonAsync(errorResponse);
        }
    }
}
