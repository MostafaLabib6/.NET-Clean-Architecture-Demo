using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Persistance.Repositories;

public class DishesRepository(RestaurantDbContext dbContext) : IDishesRepository
{
    public async Task<Dish> GetDishByRestaurantIdAndIdAsync(int restaurantId, int dishId)
    {
        return (await dbContext.Dishes.FirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.Id == dishId))!;
    }

    public async Task<int> Create(Dish dish)
    {
        dbContext.Dishes.Add(dish);
        await dbContext.SaveChangesAsync();

        return dish.Id;
    }

    public async Task DeleteDish(Dish dish)
    {
        dbContext.Dishes.Remove(dish);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}