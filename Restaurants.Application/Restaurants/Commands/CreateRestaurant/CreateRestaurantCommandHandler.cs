using Mapster;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger logger)
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Creating restaurant : {@Restaurant}", request);
        var restaurantEntity = request.Adapt<Restaurant>();
        return await restaurantsRepository.AddRestaurant(restaurantEntity);
    }
}