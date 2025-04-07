using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Services;

namespace Restaurants.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Scan & apply IRegister configurations
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IRestaurantsService, RestaurantsService>();

        services.AddScoped<IMapper, Mapper>();

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly).AddFluentValidationAutoValidation();


        return services;
    }
}