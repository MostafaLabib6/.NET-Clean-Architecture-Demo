using FluentValidation;

namespace Restaurants.Application.Identity.AssignRole;

public class AssignRoleCommandValidator: AbstractValidator<AssignRoleCommand>
{
    public AssignRoleCommandValidator()
    {
        RuleFor(command => command.RoleName).NotEmpty().WithMessage("Role name cannot be empty");
        RuleFor(command => command.Email).NotEmpty().WithMessage("User Email cannot be empty");
    }
}