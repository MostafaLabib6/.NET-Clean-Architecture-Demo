using MediatR;

namespace Restaurants.Application.Identity.AssignRole;

public class AssignRoleCommand:IRequest
{
    public string Email { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}

