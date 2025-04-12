using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Commands.CreateRestaurantDish;
using Restaurants.Application.Dishes.Commands.DeleteRestaurantDish;
using Restaurants.Application.Dishes.Commands.UpdateRestaurantDish;
using Restaurants.Application.Dishes.Queries;
using Restaurants.Application.Dishes.Queries.GetAllRestaurantDishes;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> Get([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetAllRestaurantDishesQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetDishById(int restaurantId, int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpPost]
        public async Task Post(int restaurantId, [FromBody] CreateRestaurantDishCommand dish)
        {
            dish.RestaurantId = restaurantId;
            var dishId = await mediator.Send(dish);

            CreatedAtAction(nameof(GetDishById), new { restaurantId, dishId }, null);
        }

        [HttpPut("{dishId}")]
        public async Task<IActionResult> Put(int restaurantId, int dishId, [FromBody] UpdateRestaurantDishCommand dish)
        {
            dish.Id = dishId;
            dish.RestaurantId = restaurantId;
            await mediator.Send(dish);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int restaurantId, int id)
        {
            await mediator.Send(new DeleteRestaurantDishCommand(restaurantId, id));

            return NoContent();
        }
    }
}