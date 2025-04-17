using Mapster;
using MediatR;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, ILogger logger, IUserContext userContext)
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Creating restaurant : {@Restaurant}", request);
        var currentUser = userContext.GetCurrentUser();
        var restaurantEntity = request.Adapt<Restaurant>();
        restaurantEntity.OwnerId = currentUser.Id;
        return await restaurantsRepository.AddRestaurant(restaurantEntity);
    }
}