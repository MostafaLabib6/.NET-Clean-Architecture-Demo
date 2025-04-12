using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Persistance.Repositories;

internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.AsNoTracking().ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await dbContext.Restaurants.AsNoTracking().Include(x => x.Dishes).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> AddRestaurant(Restaurant restaurant)
    {
        await dbContext.Restaurants.AddAsync(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task DeleteRestaurant(Restaurant restaurant)
    {
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public Task UpdateRestaurant(int requestId, string requestName, string requestDescription, string requestCategory, bool requestHasDelivery,
        string requestPhoneNumber, string requestEmail, string? requestCity, string? requestStreet, string? requestPostalCode)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> RestaurantExists(int id)
    {
        return await dbContext.Restaurants.AnyAsync(x => x.Id == id);
    }
}