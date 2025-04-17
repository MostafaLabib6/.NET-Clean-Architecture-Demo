using FluentValidation;

namespace Restaurants.Application.Identity.Commands.RevokeRole;

public class RevokeRoleCommandValidator : AbstractValidator<RevokeRoleCommand>
{
    public RevokeRoleCommandValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");


        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Role name can only contain letters and numbers");
    }
}