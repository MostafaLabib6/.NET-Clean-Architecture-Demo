using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
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

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(int page, int pageSize, string? searchTerm, string? sortBy,
        SortDirection sortOrder)
    {
        searchTerm = searchTerm?.ToLower() ?? string.Empty;
        var query = dbContext.Restaurants.AsNoTracking()
            .Where(x => x.Name.ToLower().Contains(searchTerm) || x.Description.ToLower().Contains(searchTerm));

        Dictionary<string?, Expression<Func<Restaurant, object>>> sortByOptions = new()
        {
            { nameof(Restaurant.Name), x => x.Name },
            { nameof(Restaurant.Description), x => x.Description },
            { nameof(Restaurant.Category), x => x.Category },
        };

        if (sortBy is not null && sortByOptions.ContainsKey(sortBy))
        {
            query = sortOrder == SortDirection.DESC ? query.OrderByDescending(sortByOptions[sortBy]) : query.OrderBy(sortByOptions[sortBy]);
        }


        var totalCount = await query.CountAsync();
        var restaurants = await 
            query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        
        return (restaurants, totalCount);
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