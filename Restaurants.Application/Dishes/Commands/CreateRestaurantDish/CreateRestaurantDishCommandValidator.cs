using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateRestaurantDish;

public class CreateRestaurantDishCommandValidator : AbstractValidator<CreateRestaurantDishCommand>
{
    public CreateRestaurantDishCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}