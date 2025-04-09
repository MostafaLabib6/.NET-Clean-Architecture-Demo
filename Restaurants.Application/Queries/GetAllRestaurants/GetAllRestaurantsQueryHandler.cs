using Mapster;
using MediatR;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,ILogger logger)
    : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        return restaurants.Adapt<IEnumerable<RestaurantDto>>();
    }
}