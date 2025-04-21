using MediatR;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Queries.GetrestaurantsById;

public class GetRestaurantsByIdQuery(int id) : IRequest<RestaurantDto>
{
    public int Id { get; } = id;
}