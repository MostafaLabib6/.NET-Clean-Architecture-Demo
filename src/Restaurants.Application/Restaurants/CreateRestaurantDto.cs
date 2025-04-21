namespace Restaurants.Application.Restaurants;

public class CreateRestaurantDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;

    public bool HasDelivery { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}