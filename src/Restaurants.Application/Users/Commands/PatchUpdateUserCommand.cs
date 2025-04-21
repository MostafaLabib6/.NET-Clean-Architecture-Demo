using MediatR;

namespace Restaurants.Application.Users;

public class PatchUpdateUserCommand : IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string Nationality { get; set; } = default!;
}