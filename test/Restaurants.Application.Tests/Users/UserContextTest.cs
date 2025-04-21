using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Tests.Users;

public class UserContextTest
{
    [Fact]
    public void GetCurrentUser_WithAuthorizedUser_ShouldReturnCurrentUser()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@test.com"),
            new(ClaimTypes.Role, nameof(RolesEnum.ADMIN)),
            new(ClaimTypes.Role, nameof(RolesEnum.USER))
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthType"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // Act
        var currentUser = userContext.GetCurrentUser();

        // Assert
        Assert.NotNull(currentUser);
        Assert.Equal("1", currentUser.Id);
        Assert.Equal("test@test.com", currentUser.Email);
        currentUser.Roles.Should().ContainInOrder(nameof(RolesEnum.ADMIN), nameof(RolesEnum.USER));
    }

    [Fact]
    public void GetCurrentUser_WithUnAuthorizedUser_ShouldReturnCurrentUser()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>();

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthType"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // Act
        Action action = () => userContext.GetCurrentUser();

        // Assert
        action.Should().Throw<UnauthorizedAccessException>()
            .WithMessage("User is not authenticated");
    }
}