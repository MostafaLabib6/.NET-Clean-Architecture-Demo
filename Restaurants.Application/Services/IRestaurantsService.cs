using Restaurants.Domain.Entities;

namespace Restaurants.Application.Services;

public interface IRestaurantsService
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
}