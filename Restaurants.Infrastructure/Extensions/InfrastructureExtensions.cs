using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Seeder;

namespace Restaurants.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<RestaurantDb>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RestaurantsDb")));

        services.AddScoped<IRestaurantDataSeeder, RestaurantDataSeeder>();
        
        
        return services;
    }
}