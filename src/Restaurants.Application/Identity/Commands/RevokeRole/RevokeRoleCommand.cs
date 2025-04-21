using MediatR;

namespace Restaurants.Application.Identity.Commands.RevokeRole;

public class RevokeRoleCommand: IRequest
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}