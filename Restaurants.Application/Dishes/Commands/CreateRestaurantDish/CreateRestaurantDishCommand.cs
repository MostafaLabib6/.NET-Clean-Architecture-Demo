using MediatR;

namespace Restaurants.Application.Dishes.Commands.CreateRestaurantDish;

public class CreateRestaurantDishCommand:IRequest<int>
{
    public int RestaurantId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}