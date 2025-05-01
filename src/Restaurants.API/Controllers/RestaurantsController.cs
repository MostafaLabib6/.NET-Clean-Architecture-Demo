using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.CreateRestaurant;
using Restaurants.Application.Commands.DeleteRestaurant;
using Restaurants.Application.Commands.UpdateRestaurant;
using Restaurants.Application.Queries.GetAllRestaurants;
using Restaurants.Application.Queries.GetrestaurantsById;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.AddRestaurantLogo;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantsByIdQuery(id));
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] CreateRestaurantCommand restaurant)
    {
        var id = await mediator.Send(restaurant);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [Authorize(Policy = PolicyNames.AtLeast20)]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurantPatch(int id, [FromBody] UpdateRestaurantPatchCommand restaurantPatch)
    {
        restaurantPatch.Id = id;
        await mediator.Send(restaurantPatch);
        return NoContent();
    }

    [HttpPost("{id}/logo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, Type = typeof(string))]
    public async Task<IActionResult> AddRestaurantLogo([FromRoute] int id, IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var restaurant = new AddRestaurantLogoCommand(id, file.FileName, stream);
        await mediator.Send(restaurant);
        return NoContent();
    }
}