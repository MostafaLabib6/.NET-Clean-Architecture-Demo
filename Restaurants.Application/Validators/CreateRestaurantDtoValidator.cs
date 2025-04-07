using FluentValidation;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Validators;

public class CreateRestaurantDtoValidator:AbstractValidator<CreateRestaurantDto>
{
    private List<string> validCategories =
    [
        "Fast Food", "Casual", "Fast Casual", "Contemporary Casual", "Fine Dining", "Cafes and Coffee Shops", "Specialty Drinks", "Buffet",
        "Food Trucks", "Concession Stands", "Pop-Ups", "Ghost Restaurants"
    ];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Name)
            .Length(5,100).NotEmpty();
        
        RuleFor(x=>x.Email)
            .EmailAddress()
            .WithMessage("Email address is not valid");
        
        RuleFor(x=>x.PhoneNumber)
            .Matches("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$")
            .WithMessage("Phone number is not valid");
        
        RuleFor(x=>x.PostalCode)
            .Matches("^\\d{2}-\\d{3}$")
            .WithMessage("Postal code is not valid (XX-XXX)");

        RuleFor(x => x.Category)
            .Must(validCategories.Contains)
            .WithMessage("Category is not valid");

    }
    
}