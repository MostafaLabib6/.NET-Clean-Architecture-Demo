using Mapster;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Dishes.Queries.GetAllRestaurantDishes;

public class GetAllRestaurantDishesQueryHandler(IRestaurantsRepository repository, ILogger logger)
    : IRequestHandler<GetAllRestaurantDishesQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetAllRestaurantDishesQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Getting all dishes for restaurant with id : {@Id}", request.RestaurantId);
        var restaurant = await repository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var result = restaurant.Dishes.Adapt<IEnumerable<DishDto>>();
        return result;
    }
}