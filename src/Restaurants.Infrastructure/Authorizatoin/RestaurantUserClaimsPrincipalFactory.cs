using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization.Constants;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantUserClaimsPrincipalFactory(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var claimsIdentity = await base.GenerateClaimsAsync(user);

        claimsIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty));
        claimsIdentity.AddClaim(new Claim(nameof(RestaurantClaimTypes.Nationality), user.Nationality ?? string.Empty));

        return new ClaimsPrincipal(claimsIdentity);
    }
}