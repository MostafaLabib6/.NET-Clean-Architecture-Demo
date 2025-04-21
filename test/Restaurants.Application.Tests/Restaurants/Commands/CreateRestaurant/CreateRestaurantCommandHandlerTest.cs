using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Mapster;
using MapsterMapper;
using Moq;
using Restaurants.Application.Commands.CreateRestaurant;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;
using Mapper = Restaurants.Application.Common.Mapper;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

[TestSubject(typeof(CreateRestaurantCommandHandler))]
public class CreateRestaurantCommandHandlerTest
{
    private readonly Mock<IRestaurantsRepository> _restaurantRepositoryMock;
    private readonly Mock<Serilog.ILogger> _loggerMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly CreateRestaurantCommandHandler _handler;
    private readonly TypeAdapterConfig _config;


    public CreateRestaurantCommandHandlerTest()
    {
        _restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        _loggerMock = new Mock<Serilog.ILogger>();
        _userContextMock = new Mock<IUserContext>();
        _config = new TypeAdapterConfig();
        new Mapper().Register(_config);
    }

    [Fact]
    public async Task CreateRestaurantHandler_AddValidRestaurant_RestaurantCreatedSuccessfully()
    {
        // Arrange
        var returnedRestaurant = new Restaurant();
        var currentUser = new CurrentUser("X_OWNER_1", "test@test.com", []);
        
        _userContextMock.Setup(x => x.GetCurrentUser()).Returns(currentUser);
        
        _restaurantRepositoryMock.Setup(x => x.AddRestaurant(It.IsAny<Restaurant>()))
            .Callback<Restaurant>(r => returnedRestaurant = r)
            .ReturnsAsync(1);

        var command = new CreateRestaurantCommand()
        {
            Name = "Test Restaurant",
            Email = "test@test.com",
            PhoneNumber = "123-456-7890",
            Description = "A fast food restaurant",
            Category = "Fast Food",
            HasDelivery = true,
            City = "Test City",
            Street = "Test Street",
            PostalCode = "12345"
        };


        // Act
        var _handler = new CreateRestaurantCommandHandler(_restaurantRepositoryMock.Object, _loggerMock.Object, _userContextMock.Object);
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        _restaurantRepositoryMock.Verify(x => x.AddRestaurant(It.IsAny<Restaurant>()), Times.Once);
        returnedRestaurant.OwnerId.Should().Be(currentUser.Id);
    }
}