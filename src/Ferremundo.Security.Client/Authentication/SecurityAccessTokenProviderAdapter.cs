using Ferremundo.Integrations.Rest.Abstractions.Authentication;
using Ferremundo.Security.Client.Abstractions;

namespace Ferremundo.Security.Client.Authentication;

public sealed class SecurityAccessTokenProviderAdapter : IAccessTokenProvider
{
    private readonly ISecurityAccessTokenProvider _innerProvider;

    public SecurityAccessTokenProviderAdapter(ISecurityAccessTokenProvider innerProvider)
    {
        _innerProvider = innerProvider;
    }

    public async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        => await _innerProvider.GetAccessTokenAsync(cancellationToken);
}
