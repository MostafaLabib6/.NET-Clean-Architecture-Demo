using System.Data.Common;

namespace Restaurants.Application.Dishes;

public record DishDto(int Id, string Name, string Description, decimal Price)
{
}