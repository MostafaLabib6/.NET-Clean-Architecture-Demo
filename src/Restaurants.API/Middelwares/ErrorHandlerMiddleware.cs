using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares;

public class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "An error occurred while processing your request." });
        }
    }
}