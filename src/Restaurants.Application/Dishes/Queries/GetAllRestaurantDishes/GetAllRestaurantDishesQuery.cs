using MediatR;

namespace Restaurants.Application.Dishes.Queries.GetAllRestaurantDishes;

public class GetAllRestaurantDishesQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
}