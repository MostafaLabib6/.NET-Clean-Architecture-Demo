using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Services;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetByIdAsync(int id);
    Task<int> AddRestaurant(CreateRestaurantDto restaurant);
}