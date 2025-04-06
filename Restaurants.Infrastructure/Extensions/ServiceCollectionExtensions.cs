using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Persistence.Repositories;
using Restaurants.Infrastructure.Seeder;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RestaurantsDb")));

        services.AddScoped<IRestaurantDataSeeder, RestaurantDataSeeder>();

        services.AddScoped<IRestaurantsRepository,RestaurantsRepository>();
        
        
        return services;
    }
}