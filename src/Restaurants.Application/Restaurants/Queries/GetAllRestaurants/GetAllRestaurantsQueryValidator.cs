using FluentValidation;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private List<int> allowPageSizes = new() { 5, 10, 15, 20 };

    private string[] allowedSortByColumnNames =
    [
        nameof(RestaurantDto.Name),
        nameof(RestaurantDto.Category),
        nameof(RestaurantDto.Description)
    ];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize).Must(x => allowPageSizes.Contains(x))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(x => x.SortBy)
            .Must(x => allowedSortByColumnNames.Contains(x))
            .When(q => q.SortBy is not null)
            .WithMessage("SortBy must be one of the following: Name, Email, PhoneNumber, PostalCode, Category");
    }
}