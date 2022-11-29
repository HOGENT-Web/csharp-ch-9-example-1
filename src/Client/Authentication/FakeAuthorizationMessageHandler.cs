using System;
using System.Security.Claims;

namespace BogusStore.Client.Authentication;

public class FakeAuthorizationMessageHandler : DelegatingHandler
{
    private readonly FakeAuthenticationProvider fakeAuthenticationProvider;

    public FakeAuthorizationMessageHandler(FakeAuthenticationProvider fakeAuthenticationProvider)
    {
        this.fakeAuthenticationProvider = fakeAuthenticationProvider;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        if(fakeAuthenticationProvider.Current.Identity?.Name == FakeAuthenticationProvider.Anonymous.Identity?.Name)
        {
            return base.SendAsync(request, cancellationToken);
        }

        request.Headers.Add("UserId", fakeAuthenticationProvider.Current.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        request.Headers.Add("Role", fakeAuthenticationProvider.Current.FindFirst(ClaimTypes.Role)?.Value);
        request.Headers.Add("Email", fakeAuthenticationProvider.Current.FindFirst(ClaimTypes.Email)?.Value);
        request.Headers.Add("Name", fakeAuthenticationProvider.Current.FindFirst(ClaimTypes.Name)?.Value);
        return base.SendAsync(request, cancellationToken);
    }
}

