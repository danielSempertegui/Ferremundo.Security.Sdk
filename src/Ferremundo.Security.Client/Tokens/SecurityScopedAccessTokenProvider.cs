using Ferremundo.Integrations.Rest.Abstractions.Authentication;
using Ferremundo.Security.Client.Abstractions.Tokens;

namespace Ferremundo.Security.Client.Tokens;

public sealed class SecurityScopedAccessTokenProvider : IAccessTokenProvider
{
    private readonly ISecurityClientCredentialsTokenProvider _tokenProvider;
    private readonly string _scope;

    public SecurityScopedAccessTokenProvider(
        ISecurityClientCredentialsTokenProvider tokenProvider,
        string scope)
    {
        _tokenProvider = tokenProvider;
        _scope = scope;
    }

    public async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        => await _tokenProvider.GetAccessTokenAsync(_scope, cancellationToken);
}
