
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Services;

namespace Restaurants.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantsService, RestaurantsService>();
        return services;
    }
}