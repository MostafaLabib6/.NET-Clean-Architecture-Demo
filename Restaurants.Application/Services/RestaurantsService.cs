using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository) : IRestaurantsService
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await restaurantsRepository.GetAllAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await restaurantsRepository.GetByIdAsync(id);
    }
}