using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Identity.Commands.RevokeRole;

public class RevokeRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<RevokeRoleCommand>
{
    public async Task Handle(RevokeRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.UserEmail) ??
                   throw new NotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName) ??
                   throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        var result = await userManager.RemoveFromRoleAsync(user, role.Name);

        if (!result.Succeeded)
        {
            throw new Exception($"Failed to assign role {role.Name} to user {user.Email}");
        }
    }
}