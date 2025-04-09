using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var startTime = Stopwatch.StartNew();
        await next.Invoke(context);
        startTime.Stop();

        if (startTime.ElapsedMilliseconds / 1000 > 4)
            logger.LogWarning($"RequestTime: {startTime.ElapsedMilliseconds / 1000} seconds with {context.Request.Method} {context.Request.Path}");
    }
}