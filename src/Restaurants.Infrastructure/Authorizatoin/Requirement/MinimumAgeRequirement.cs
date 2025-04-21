using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirement;

public class MinimumAgeRequirement(int minAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minAge;
}