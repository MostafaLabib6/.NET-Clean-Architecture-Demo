using MediatR;

namespace Restaurants.Application.Dishes.Queries;

public class GetDishByIdQuery(int restaurantId, int dishId) :
    IRequest<DishDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
}