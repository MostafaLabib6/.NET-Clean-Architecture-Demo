using Mapster;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Dishes.Queries.GetDishById;

public class GetDishByIdQueryHandler(IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, ILogger logger)
    : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Getting dish with id : {@Id} for restaurant with id : {@RestaurantId}", request.DishId, request.RestaurantId);
        if (!await restaurantsRepository.RestaurantExists(request.RestaurantId))
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = await dishesRepository.GetDishByRestaurantIdAndIdAsync(request.RestaurantId, request.DishId);

        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        return dish.Adapt<DishDto>();
    }
}