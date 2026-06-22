using Ferremundo.Integrations.Rest.Authentication;
using Ferremundo.Security.Client.Abstractions;

namespace Ferremundo.Security.Client.Authentication;

public sealed class SecurityClientAuthenticationStrategy : ISecurityClientAuthenticationStrategy
{
    private readonly BearerTokenAuthenticationStrategy _innerStrategy;

    public SecurityClientAuthenticationStrategy(ISecurityAccessTokenProvider accessTokenProvider)
    {
        _innerStrategy = new BearerTokenAuthenticationStrategy(new SecurityAccessTokenProviderAdapter(accessTokenProvider));
    }

    public Task ApplyAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        => _innerStrategy.ApplyAsync(request, cancellationToken);
}
