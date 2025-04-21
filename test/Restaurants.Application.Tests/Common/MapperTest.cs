using FluentAssertions;
using Mapster;
using Restaurants.Application.Commands.CreateRestaurant;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.Commands.CreateRestaurantDish;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Tests.Common;

public class MapperTest
{
    private readonly TypeAdapterConfig _config;

    public MapperTest()
    {
        _config = new TypeAdapterConfig();
        new Mapper().Register(_config);
    }

    [Fact]
    public void CreateMap_RestaurantToRestaurantDto_ValidMapping()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Address = new Address
            {
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"
            },
            Category = "Fast Food",
            Description = "A fast food restaurant",
            PhoneNumber = "123-456-7890",
            HasDelivery = true,
            Email = "test@test.com",
            OwnerId = 1.ToString(),
        };

        // Act
        var restaurantDto = restaurant.Adapt<RestaurantDto>(_config);

        // Assert
        Assert.NotNull(restaurantDto);
        Assert.Equal(restaurant.Id, restaurantDto.Id);
        Assert.Equal(restaurant.Name, restaurantDto.Name);
        Assert.Equal(restaurant.Address.City, restaurantDto.City);
        Assert.Equal(restaurant.Address.Street, restaurantDto.Street);
        Assert.Equal(restaurant.Address.PostalCode, restaurantDto.PostalCode);
        Assert.Equal(restaurant.Category, restaurantDto.Category);
        Assert.Equal(restaurant.Description, restaurantDto.Description);
        Assert.Equal(restaurant.HasDelivery, restaurantDto.HasDelivery);
    }

    [Fact]
    public void CreateRestaurantCommand_To_Restaurant_MapsCorrectly()
    {
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name = "New Restaurant",
            Description = "New Description",
            City = "New City",
            Street = "New Street",
            PostalCode = "54321"
        };

        // Act
        var restaurant = command.Adapt<Restaurant>(_config);

        // Assert
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.Address.Should().NotBeNull();
        restaurant.Address.City.Should().Be(command.City);
        restaurant.Address.Street.Should().Be(command.Street);
        restaurant.Address.PostalCode.Should().Be(command.PostalCode);
    }

    [Fact]
    public void CreateRestaurantDishCommand_To_Dish_MapsCorrectly()
    {
        // Arrange
        var command = new CreateRestaurantDishCommand
        {
            Name = "New Dish",
            Description = "New Dish Description",
            Price = 15.99m,
            RestaurantId = 10
        };

        // Act
        var dish = command.Adapt<Dish>(_config);

        // Assert
        dish.Name.Should().Be(command.Name);
        dish.Description.Should().Be(command.Description);
        dish.Price.Should().Be(command.Price);
        dish.RestaurantId.Should().Be(command.RestaurantId);
    }

    [Fact]
    public void Restaurant_WithNullAddress_To_RestaurantDto_MapsCorrectly()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Description",
            Address = null
        };

        // Act
        var dto = restaurant.Adapt<RestaurantDto>(_config);

        // Assert
        dto.Id.Should().Be(restaurant.Id);
        dto.Name.Should().Be(restaurant.Name);
        dto.Description.Should().Be(restaurant.Description);
        dto.City.Should().BeNull();
        dto.Street.Should().BeNull();
        dto.PostalCode.Should().BeNull();
    }
}