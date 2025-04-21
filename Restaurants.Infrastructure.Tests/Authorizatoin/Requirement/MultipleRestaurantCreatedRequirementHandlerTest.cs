using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirement;
using Xunit;

namespace Restaurants.Infrastructure.Tests.Authorization.Requirement;

[TestSubject(typeof(MultipleRestaurantCreatedRequirementHandler))]
public class MultipleRestaurantCreatedRequirementHandlerTest
{
    private readonly Mock<IUserContext> _userContext = new();
    private readonly Mock<IRestaurantsRepository> _restaurantsRepository = new();

    [Fact]
    public async Task MultipleRestaurantsRequirementHandler_UserHasCreatedMultipleRestaurant_ShouldSuccess()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", []);
        _userContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        _restaurantsRepository.Setup(x => x.GetNumberOfRestaurantsByOwnerId(currentUser.Id))
            .ReturnsAsync(2);

        var requirement = new MultipleRestaurantCreatedRequirement(2);

        var handlerContext = new AuthorizationHandlerContext([requirement],
            new DefaultHttpContext().User,
            null);

        // Act
        var handler = new MultipleRestaurantCreatedRequirementHandler(_userContext.Object, _restaurantsRepository.Object);
        await handler.HandleAsync(handlerContext);
        
        // Assert
        handlerContext.HasSucceeded.Should().BeTrue();
    }
    
    [Fact]
    public async Task MultipleRestaurantsRequirementHandler_UserHasNotCreatedMultipleRestaurant_ShouldSuccess()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", []);
        _userContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);

        _restaurantsRepository.Setup(x => x.GetNumberOfRestaurantsByOwnerId(currentUser.Id))
            .ReturnsAsync(1);

        var requirement = new MultipleRestaurantCreatedRequirement(2);

        var handlerContext = new AuthorizationHandlerContext([requirement],
            new DefaultHttpContext().User,
            null);

        // Act
        var handler = new MultipleRestaurantCreatedRequirementHandler(_userContext.Object, _restaurantsRepository.Object);
        await handler.HandleAsync(handlerContext);
        
        // Assert
        handlerContext.HasSucceeded.Should().BeFalse();
        handlerContext.HasFailed.Should().BeTrue();
    }
}