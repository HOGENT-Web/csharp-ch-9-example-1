using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BogusStore.Server.Authentication;

public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeAuthenticationHandler(
      IOptionsMonitor<AuthenticationSchemeOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock)
    : base(options, logger, encoder, clock) { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        List<Claim> claims = new();

        if (Context.Request.Headers.TryGetValue("UserId", out var userId))
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId!));
        }

        if (Context.Request.Headers.TryGetValue("Role", out var roles))
        {
            claims.Add(new Claim(ClaimTypes.Role, roles!));
        }

        if (Context.Request.Headers.TryGetValue("Email", out var email))
        {
            claims.Add(new Claim(ClaimTypes.Email, email!));
        }

        if (Context.Request.Headers.TryGetValue("Name", out var name))
        {
            claims.Add(new Claim(ClaimTypes.Name, name!));
        }

        if (claims.Any())
        {
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }

        return await Task.FromResult(AuthenticateResult.NoResult());

    }
}

