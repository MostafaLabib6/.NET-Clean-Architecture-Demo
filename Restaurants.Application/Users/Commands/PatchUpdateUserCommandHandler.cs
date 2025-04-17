using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Serilog;

namespace Restaurants.Application.Users;

public class PatchUpdateUserCommandHandler(ILogger logger, IUserContext userContext, IUserStore<User> userStore)
    : IRequestHandler<PatchUpdateUserCommand>
{
    public async Task Handle(PatchUpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.Information("Updating user with Id {user.Id}: {@User}", user.Id, request);

        var userDb = await userStore.FindByIdAsync(user.Id, cancellationToken);

        if (userDb == null)
            throw new NotFoundException(nameof(User), user.Id);

        userDb.Nationality = request.Nationality;
        userDb.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(userDb, cancellationToken);
    }
}