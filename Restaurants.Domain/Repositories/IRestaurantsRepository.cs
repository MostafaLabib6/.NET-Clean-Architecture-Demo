using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> AddRestaurant(Restaurant restaurant);
    Task DeleteRestaurant(Restaurant restaurant);

    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(int page, int pageSize, string? searchTerm, string? sortBy,
        SortDirection sortOrder);

    Task UpdateRestaurant(int requestId, string requestName, string requestDescription, string requestCategory, bool requestHasDelivery,
        string requestPhoneNumber, string requestEmail, string? requestCity, string? requestStreet, string? requestPostalCode);

    Task SaveChangesAsync();
    Task<bool> RestaurantExists(int id);
}