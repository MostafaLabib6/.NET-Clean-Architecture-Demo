using Mapster;
using MediatR;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Queries.GetrestaurantsById;

public class GetRestaurantsByIdQueryHandler(IRestaurantsRepository restaurantsRepository, ILogger logger)
    : IRequestHandler<GetRestaurantsByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantsByIdQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Getting restaurant with id : {@Id}", request.Id);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        return restaurant.Adapt<RestaurantDto>();
    }
}