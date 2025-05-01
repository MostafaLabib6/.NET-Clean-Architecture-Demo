using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Constants;
using Restaurants.Infrastructure.Authorization.Requirement;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Persistance.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeder;
using Restaurants.Infrastructure.ServiceConfiguration;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RestaurantsDb")));

        services.AddScoped<IRestaurantDataSeeder, RestaurantDataSeeder>();

        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        services.AddScoped<IAuthorizationHandler, MultipleRestaurantCreatedRequirementHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyNames.HasNationality, policy => policy.RequireClaim(nameof(RestaurantClaimTypes.Nationality), "Egyptian"));
            options.AddPolicy(PolicyNames.AtLeast20, policy =>
                policy.Requirements.Add(new MinimumAgeRequirement(20)));
            options.AddPolicy(PolicyNames.MultipleRestaurantCreated, policy => policy.AddRequirements(new MultipleRestaurantCreatedRequirement(2)));
        });

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantDbContext>();

        services.Configure<BlogStorageSettings>(configuration.GetSection("BlogStorageSettings"));
        services.AddScoped<IBlobStorageService, BlogStorageService>();
        return services;
    }
}