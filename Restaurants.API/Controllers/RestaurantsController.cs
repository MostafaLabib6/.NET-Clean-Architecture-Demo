using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.CreateRestaurant;
using Restaurants.Application.Commands.DeleteRestaurant;
using Restaurants.Application.Commands.UpdateRestaurant;
using Restaurants.Application.Queries.GetAllRestaurants;
using Restaurants.Application.Queries.GetrestaurantsById;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantsByIdQuery(id));
        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantCommand restaurant)
    {
        var id = await mediator.Send(restaurant);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
        if (isDeleted)
            return NoContent();
        return NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurantPatch(int id, [FromBody] UpdateRestaurantPatchCommand restaurantPatch)
    {
        restaurantPatch.Id = id;
        var isUpdated = await mediator.Send(restaurantPatch);
        if (isUpdated)
            return NoContent();
        return NotFound();
    }
}