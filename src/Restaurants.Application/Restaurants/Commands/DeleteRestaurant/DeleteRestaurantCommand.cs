using MediatR;

namespace Restaurants.Application.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; } = id;
}