using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    public Task<bool> IsUserAuthorized(Restaurant restaurant,ResourceOperation resourceOperation);
}