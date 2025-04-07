// using Mapster;
// using MapsterMapper;
// using Restaurants.Application.Restaurants;
// using Restaurants.Domain.Entities;
// using Restaurants.Domain.Repositories;
//
// namespace Restaurants.Application.Services;
//
// internal class RestaurantsService(IRestaurantsRepository _restaurantsRepository,IMapper _mapper) : IRestaurantsService
// {
//     public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
//     {
//         var restaurants =await _restaurantsRepository.GetAllAsync();
//         return restaurants.Adapt<IEnumerable<RestaurantDto>>();
//     }
//     public async Task<RestaurantDto?> GetByIdAsync(int id)
//     {
//         var restaurant =  await _restaurantsRepository.GetByIdAsync(id);
//         return restaurant.Adapt<RestaurantDto>();
//     }
//
//     public async Task<int> AddRestaurant(CreateRestaurantDto restaurant)
//     {
//         var restaurantEntity = restaurant.Adapt<Restaurant>();
//         return await _restaurantsRepository.AddRestaurant(restaurantEntity);
//     }
// }