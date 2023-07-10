using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace USite.Infrastructure.Identity;

public class CustomRedirectUriValidator : IRedirectUriValidator
{
    public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
    {
        return ValidUrl(client.RedirectUris, requestedUri);
    }

    public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
    {

        return ValidUrl(client.RedirectUris, requestedUri);
    }

    private Task<bool> ValidUrl(ICollection<string> uris, string requestUri)
    {
        var uri = requestUri.Split('?')[0];

        var x = uris.Any(x => x == uri);

        return Task.FromResult(x);
    }
}