using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository repository,ILogger logger) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Deleting restaurant with id : {Id}", request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);
        logger.Information("Restaurant : {restaurant} has been deleted", restaurant);
        
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        await repository.DeleteRestaurant(restaurant);
    }
}