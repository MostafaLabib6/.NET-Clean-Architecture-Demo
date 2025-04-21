using Mapster;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Commands.UpdateRestaurant;

public class UpdateRestaurantPatchCommandHandler(IRestaurantsRepository repository, ILogger logger) : IRequestHandler<UpdateRestaurantPatchCommand>
{
    public async Task Handle(UpdateRestaurantPatchCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Updating restaurant : {Restaurant}", request);
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        request.Adapt(restaurant);

        await repository.SaveChangesAsync();
    }
}