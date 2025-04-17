using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users;

public interface IUserContext
{
    CurrentUser GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user?.Identity == null || !user.Identity.IsAuthenticated) throw new UnauthorizedAccessException("User is not authenticated");

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = user.FindFirstValue(ClaimTypes.Email);
        var roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
 
        return new CurrentUser(userId, email, roles);
    }
}