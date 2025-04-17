using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Serilog;

namespace Restaurants.Infrastructure.Authorization.Requirement;

public class MinimumAgeRequirementHandler(ILogger logger,IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        logger.Information("Checking minimum age requirement for user: {@User}", context.User.Identity?.Name);
        var birthOfDate = context.User.Claims.Where(x => x.Type == ClaimTypes.DateOfBirth).Select(x => x.Value);
        if (string.IsNullOrEmpty(birthOfDate.ToString()))
        {
            logger.Warning("User does not have a date of birth claim");
            context.Fail();
        }

        if (DateTime.Now.Year - DateTime.Parse(birthOfDate!.First()).Year >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }
        else
        {
            logger.Warning("User does not meet the minimum age requirement");
            context.Fail();
        }

        return Task.CompletedTask;
    }
}