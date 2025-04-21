using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirement;

public class MultipleRestaurantCreatedRequirement(int minRestaurantCount) : IAuthorizationRequirement
{
    public int MinimumRestaurantCount { get; } = minRestaurantCount;
}