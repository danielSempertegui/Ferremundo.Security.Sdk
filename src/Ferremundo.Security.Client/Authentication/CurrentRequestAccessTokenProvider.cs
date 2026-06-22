using Ferremundo.Security.Client.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Ferremundo.Security.Client.Authentication;

public sealed class CurrentRequestAccessTokenProvider : ISecurityAccessTokenProvider
{
    private const string AuthorizationHeaderName = "Authorization";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentRequestAccessTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers[AuthorizationHeaderName].ToString();
        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            return ValueTask.FromResult<string?>(null);
        }

        const string bearerPrefix = "Bearer ";
        if (!authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return ValueTask.FromResult<string?>(null);
        }

        var token = authorizationHeader[bearerPrefix.Length..].Trim();
        return ValueTask.FromResult<string?>(string.IsNullOrWhiteSpace(token) ? null : token);
    }
}
