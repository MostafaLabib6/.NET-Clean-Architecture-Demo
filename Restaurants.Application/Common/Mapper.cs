using Mapster;
using Restaurants.Application.Commands.CreateRestaurant;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Commands.CreateRestaurantDish;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Common;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Restaurant, RestaurantDto>()
            .Map(dest => dest.City, src => src.Address == null ? null : src.Address.City)
            .Map(dest => dest.Street, src => src.Address == null ? null : src.Address.Street)
            .Map(dest => dest.PostalCode, src => src.Address == null ? null : src.Address.PostalCode);


        config.NewConfig<Dish, DishDto>();


        // config.NewConfig<CreateRestaurantDto, Restaurant>()
        //     .Map(dest => dest.Address.City, src => src.City)
        //     .Map(dest => dest.Address.Street, src => src.Street)
        //     .Map(dest => dest.Address.PostalCode, src => src.PostalCode);

        config.NewConfig<CreateRestaurantCommand, Restaurant>()
            .Map(dest => dest.Address, src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            });

        config.NewConfig<CreateRestaurantDishCommand, Dish>();
    }
}