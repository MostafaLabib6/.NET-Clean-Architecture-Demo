using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteRestaurantDish;

public class DeleteRestaurantDishCommand(int restaurantId,int id): IRequest
{
    public int Id { get; } = id;
    public int RestaurantId { get; } = restaurantId;
}