using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Serilog;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger logger, UserContext userContext) : IRestaurantAuthorizationService
{
    public Task<bool> IsUserAuthorized(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.Information("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
            currentUser.Email,
            resourceOperation,
            restaurant.Name);

        if (resourceOperation == ResourceOperation.READ || resourceOperation == ResourceOperation.CREATE)
        {
            logger.Information("Create/read operation - successful authorization");
            return Task.FromResult(true);
        }

        if (resourceOperation == ResourceOperation.DELETE && currentUser.IsInRole(nameof(RolesEnum.ADMIN)))
        {
            logger.Information("Admin user, delete operation - successful authorization");
            return Task.FromResult(true);
        }

        if ((resourceOperation == ResourceOperation.UPDATE || resourceOperation == ResourceOperation.DELETE) && currentUser.Id == restaurant.OwnerId)
        {
            logger.Information("User is the owner of the restaurant, update/delete operation - successful authorization");
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}