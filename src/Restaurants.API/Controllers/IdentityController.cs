using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Identity.AssignRole;
using Restaurants.Application.Identity.Commands.RevokeRole;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] PatchUpdateUserCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("assign-role")]
    [Authorize(Roles = nameof(RolesEnum.ADMIN))]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("revoke-role")]
    public async Task<IActionResult> RevokeRole([FromBody] RevokeRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}