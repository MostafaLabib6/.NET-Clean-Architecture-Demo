using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Restaurants.API.Tests;

public class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claimsPrincipal = new ClaimsPrincipal();

        claimsPrincipal.AddIdentity(new ClaimsIdentity([
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim(ClaimTypes.Name, "TestUser")
        ]));
        var ticket = new AuthenticationTicket(claimsPrincipal, "TestAuthType");
        var authenticateResult = AuthenticateResult.Success(ticket);
        return Task.FromResult(authenticateResult);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context,
        object resource)
    {
        return Task.FromResult(PolicyAuthorizationResult.Success());
    }
}