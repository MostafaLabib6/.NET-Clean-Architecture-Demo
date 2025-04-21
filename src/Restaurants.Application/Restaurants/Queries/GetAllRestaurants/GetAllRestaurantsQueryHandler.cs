using Mapster;
using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Serilog;

namespace Restaurants.Application.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository, ILogger logger)
    : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.Information("Getting all restaurants");
        var (restaurants, total) =
            await restaurantsRepository.GetAllMatchingAsync(request.Page, request.PageSize, request.SearchTerm, request.SortBy, request.SortOrder);

        var restaurantsDto = restaurants.Adapt<IEnumerable<RestaurantDto>>();
        return new(total, request.PageSize, request.Page, restaurantsDto);
    }
}