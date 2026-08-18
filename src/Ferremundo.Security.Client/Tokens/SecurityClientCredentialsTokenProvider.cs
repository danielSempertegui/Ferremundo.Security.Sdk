using Ferremundo.Security.Client.Abstractions.Tokens;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Client.Clients.Tokens;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Tokens;

public sealed class SecurityClientCredentialsTokenProvider : ISecurityClientCredentialsTokenProvider
{
    private const int MinimumCacheSeconds = 1;
    private readonly IMemoryCache _memoryCache;
    private readonly SecurityClientOptions _options;
    private readonly ISecurityTokenClient _tokenClient;

    public SecurityClientCredentialsTokenProvider(
        ISecurityTokenClient tokenClient,
        IMemoryCache memoryCache,
        IOptions<SecurityClientOptions> options)
    {
        _tokenClient = tokenClient;
        _memoryCache = memoryCache;
        _options = options.Value;
    }

    public async ValueTask<string> GetAccessTokenAsync(
        string scope,
        CancellationToken cancellationToken = default)
    {
        var normalizedScope = NormalizeScope(scope);
        if (string.IsNullOrWhiteSpace(normalizedScope))
        {
            throw new InvalidOperationException("Security token scope is required.");
        }

        var cacheKey = BuildCacheKey(normalizedScope);
        if (_memoryCache.TryGetValue<string>(cacheKey, out var cachedToken) &&
            !string.IsNullOrWhiteSpace(cachedToken))
        {
            return cachedToken;
        }

        var token = await _tokenClient.GetClientCredentialsTokenAsync(normalizedScope, cancellationToken);
        var cacheSeconds = Math.Max(
            token.ExpiresIn - _options.TokenCacheExpirationSkewSeconds,
            MinimumCacheSeconds);

        _memoryCache.Set(
            cacheKey,
            token.AccessToken,
            TimeSpan.FromSeconds(cacheSeconds));

        return token.AccessToken;
    }

    private string BuildCacheKey(string normalizedScope)
    {
        if (string.IsNullOrWhiteSpace(_options.ClientId))
        {
            throw new InvalidOperationException("SecurityClient:ClientId is required to cache client credentials tokens.");
        }

        return $"security-token:{_options.ClientId}:{normalizedScope}";
    }

    private static string NormalizeScope(string scope)
        => string.Join(
            ' ',
            scope.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Distinct(StringComparer.Ordinal)
                .Order(StringComparer.Ordinal));
}
