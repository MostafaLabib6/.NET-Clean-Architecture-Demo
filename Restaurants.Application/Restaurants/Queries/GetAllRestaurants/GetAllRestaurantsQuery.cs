using MediatR;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
{
}