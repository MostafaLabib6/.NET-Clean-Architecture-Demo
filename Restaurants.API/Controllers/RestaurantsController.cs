using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Services;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantsService _restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await _restaurantsService.GetByIdAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantDto restaurant)
    {
        var id = await _restaurantsService.AddRestaurant(restaurant);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}