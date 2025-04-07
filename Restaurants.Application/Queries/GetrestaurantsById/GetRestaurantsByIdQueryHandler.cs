using Mapster;
using MediatR;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.GetrestaurantsById;

public class GetRestaurantsByIdQueryHandler(IRestaurantsRepository restaurantsRepository):IRequestHandler<GetRestaurantsByIdQuery,RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantsByIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant =  await restaurantsRepository.GetByIdAsync(request.Id);
        return restaurant.Adapt<RestaurantDto>();

    }
}