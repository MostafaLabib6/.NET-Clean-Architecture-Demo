using Mapster;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository) :IRequestHandler<CreateRestaurantCommand,int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantEntity = request.Adapt<Restaurant>();
        return await restaurantsRepository.AddRestaurant(restaurantEntity);   
    }
}