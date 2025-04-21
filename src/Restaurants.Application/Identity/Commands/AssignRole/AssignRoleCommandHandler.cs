using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Identity.AssignRole;

public class AssignRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignRoleCommand>
{
    public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ??
                   throw new NotFoundException(nameof(User), request.Email);

        var role = await roleManager.FindByNameAsync(request.RoleName) ??
                   throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        var result = await userManager.AddToRoleAsync(user, role.Name);

        if (!result.Succeeded)
        {
            throw new Exception($"Failed to assign role {role.Name} to user {user.Email}");
        }
    }
}