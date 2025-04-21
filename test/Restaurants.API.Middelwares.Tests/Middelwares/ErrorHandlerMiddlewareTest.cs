using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.API.Middlewares;
using Xunit;

namespace Restaurants.API.Middelwares.Tests;

[TestSubject(typeof(ErrorHandlerMiddleware))]
public class ErrorHandlerMiddlewareTest
{
    private readonly Mock<ILogger<ErrorHandlerMiddleware>> _loggerMock = new();
    private readonly Mock<HttpContext> _httpContextMock = new();
    private readonly Mock<RequestDelegate> _nextMock = new();

    [Fact]
    public void ErrorHandler_NoExceptionThrown_InvokeNext()
    {
        // Act
        var errorHandlerMiddleware = new ErrorHandlerMiddleware(_loggerMock.Object);
        errorHandlerMiddleware.InvokeAsync(_httpContextMock.Object, _nextMock.Object);

        // Assert
        _nextMock.Verify(next => next.Invoke(_httpContextMock.Object), Times.Once);
    }
}