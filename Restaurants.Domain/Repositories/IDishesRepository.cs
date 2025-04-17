using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<Dish> GetDishByRestaurantIdAndIdAsync(int restaurantId, int dishId);
    Task<int> Create(Dish dish);
    Task DeleteDish(Dish dish);
    Task SaveChangesAsync();
}