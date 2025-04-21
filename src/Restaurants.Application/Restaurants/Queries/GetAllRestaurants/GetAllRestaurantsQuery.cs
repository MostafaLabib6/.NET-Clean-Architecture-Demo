using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public string? SearchTerm { get; set; }

    public SortDirection SortOrder { get; set; }
    public string? SortBy { get; set; }
}