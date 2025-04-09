using Mapster;
using MediatR;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Commands.UpdateRestaurant;

public class UpdateRestaurantPatchCommandHandler(IRestaurantsRepository repository,ILogger logger) : IRequestHandler<UpdateRestaurantPatchCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantPatchCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Updating restaurant : {Restaurant}", request);
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null)
            return false;

        request.Adapt(restaurant);

        await repository.SaveChangesAsync();
        return true;
    }
}