using MediatR;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantsRepository repository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null)
            return false;

        await repository.DeleteRestaurant(restaurant);
        return true;
    }
}