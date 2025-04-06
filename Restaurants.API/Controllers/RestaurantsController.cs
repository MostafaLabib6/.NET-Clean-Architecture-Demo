using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Services;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var restaurants = restaurantsService.GetAllAsync();
        return Ok(restaurants);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await restaurantsService.GetByIdAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }
}