using MediatR;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Queries.GetrestaurantsById;

public class GetRestaurantsByIdQuery : IRequest<RestaurantDto?>
{
    public int Id { get; init; }
}