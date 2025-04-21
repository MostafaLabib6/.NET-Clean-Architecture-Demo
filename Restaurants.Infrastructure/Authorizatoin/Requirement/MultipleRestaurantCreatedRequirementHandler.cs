using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirement;

internal class MultipleRestaurantCreatedRequirementHandler(IUserContext userContext, IRestaurantsRepository repository)
    : AuthorizationHandler<MultipleRestaurantCreatedRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MultipleRestaurantCreatedRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        var numberOfRestaurants = await repository.GetNumberOfRestaurantsByOwnerId(currentUser.Id);

        if (numberOfRestaurants >= requirement.MinimumRestaurantCount)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}