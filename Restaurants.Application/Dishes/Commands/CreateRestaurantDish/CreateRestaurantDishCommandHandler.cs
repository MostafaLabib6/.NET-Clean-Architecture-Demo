using Mapster;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Dishes.Commands.CreateRestaurantDish;

public class CreateRestaurantDishCommandHandler(IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, ILogger logger)
    : IRequestHandler<CreateRestaurantDishCommand, int>
{
    public async Task<int> Handle(CreateRestaurantDishCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Creating dish : {@Dish}", request);
        if (!await restaurantsRepository.RestaurantExists(request.RestaurantId))
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = request.Adapt<Dish>();

        var id = await dishesRepository.Create(dish);

        return id;
    }
}