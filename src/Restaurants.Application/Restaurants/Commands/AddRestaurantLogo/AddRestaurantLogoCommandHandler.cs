using MediatR;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Restaurants.Commands.AddRestaurantLogo;

public class AddRestaurantLogoCommandHandler(ILogger logger, IRestaurantAuthorizationService authorizationService, IRestaurantsRepository repository,IBlobStorageService blobStorageService)
    : IRequestHandler<AddRestaurantLogoCommand>
{
    public async Task Handle(AddRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        logger.Information("Adding logo for restaurant with ID {Id}", request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);

        if (restaurant == null)
        {
            logger.Warning("Restaurant with ID {Id} not found", request.Id);
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        if (!await authorizationService.IsUserAuthorized(restaurant, ResourceOperation.UPDATE))
        {
            throw new ForbidException();
        }
        
        var uri = await blobStorageService.UploadAsync(request.Content,request.FileName);
        
        restaurant.LogoUrl = uri;
        
        await repository.SaveChangesAsync();
    }
}