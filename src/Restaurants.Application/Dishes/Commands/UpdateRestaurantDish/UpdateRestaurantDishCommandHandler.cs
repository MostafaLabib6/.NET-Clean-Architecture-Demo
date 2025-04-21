using Mapster;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Dishes.Commands.UpdateRestaurantDish;

public class UpdateRestaurantDishCommandHandler(IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, ILogger logger)
    : IRequestHandler<UpdateRestaurantDishCommand>
{
    public async Task Handle(UpdateRestaurantDishCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Updating dish with id : {@DishId} for restaurant with id : {@RestaurantId}", request.Id, request.RestaurantId);
        if (!await restaurantsRepository.RestaurantExists(request.RestaurantId))
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = await dishesRepository.GetDishByRestaurantIdAndIdAsync(request.RestaurantId, request.Id);
        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.Id.ToString());

        request.Adapt(dish);

        await dishesRepository.SaveChangesAsync();
    }
}