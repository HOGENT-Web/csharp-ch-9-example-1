using System;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Security.Claims;
using BogusStore.Shared.Authentication;

namespace BogusStore.Client.Authentication;

public class FakeAuthenticationProvider : AuthenticationStateProvider
{
    public static ClaimsPrincipal Anonymous => new(new ClaimsIdentity(new[]
    {
            new Claim(ClaimTypes.Name, "Anonymous"),
    }));

    public static ClaimsPrincipal Administrator => new(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim(ClaimTypes.Name, "Administrator"),
        new Claim(ClaimTypes.Email, "fake-administrator@gmail.com"),
        new Claim(ClaimTypes.Role, Roles.Administrator),
    }, "Fake Authentication"));

    public static ClaimsPrincipal Customer => new(new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, "2"),
        new Claim(ClaimTypes.Name, "Customer"),
        new Claim(ClaimTypes.Email, "fake-customer@gmail.com"),
        new Claim(ClaimTypes.Role, Roles.Customer),
    }, "Fake Authentication"));

    public static IEnumerable<ClaimsPrincipal> ClaimsPrincipals =>
        new List<ClaimsPrincipal>() { Anonymous, Customer, Administrator }; 

    public ClaimsPrincipal Current { get; private set; } = Administrator;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(Current));
    }

    public void ChangeAuthenticationState(ClaimsPrincipal claimsPrincipal)
    {
        Current = claimsPrincipal;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
