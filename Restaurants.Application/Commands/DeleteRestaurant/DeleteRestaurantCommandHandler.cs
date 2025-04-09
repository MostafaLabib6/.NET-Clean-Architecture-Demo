using MediatR;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository repository,ILogger logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Deleting restaurant with id : {Id}", request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);
        logger.Information("Restaurant : {restaurant} has been deleted", restaurant);
        
        if (restaurant is null)
            return false;

        await repository.DeleteRestaurant(restaurant);
        return true;
    }
}