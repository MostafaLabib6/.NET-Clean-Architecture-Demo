using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Dishes.Commands.DeleteRestaurantDish;

public class DeleteRestaurantDishCommandHandler(IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, ILogger logger)
    : IRequestHandler<DeleteRestaurantDishCommand>
{
    public async Task Handle(DeleteRestaurantDishCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Deleting dish with id : {Id} for restaurant with id : {RestaurantId}", request.Id, request.RestaurantId);
        if (!await restaurantsRepository.RestaurantExists(request.RestaurantId))
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = await dishesRepository.GetDishByRestaurantIdAndIdAsync(request.RestaurantId, request.Id);

        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.Id.ToString());

        await dishesRepository.DeleteDish(dish);
    }
}