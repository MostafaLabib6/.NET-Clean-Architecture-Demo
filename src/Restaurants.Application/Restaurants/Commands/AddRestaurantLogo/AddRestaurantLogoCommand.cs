using MediatR;

namespace Restaurants.Application.Restaurants.Commands.AddRestaurantLogo;

public class AddRestaurantLogoCommand(int id, string fileName, Stream content) : IRequest
{
    public int Id { get; } = id;
    public string FileName { get; } = fileName;
    public Stream Content { get; } = content;
}