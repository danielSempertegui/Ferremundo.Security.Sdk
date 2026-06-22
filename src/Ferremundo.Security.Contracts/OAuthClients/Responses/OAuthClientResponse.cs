namespace Ferremundo.Security.Contracts.OAuthClients.Responses;

public sealed class OAuthClientResponse
{
    public Guid ClientGuid { get; init; }

    public string ClientId { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public string ClientType { get; init; } = string.Empty;

    public bool RequiresPkce { get; init; }

    public bool AllowRefreshTokens { get; init; }

    public bool HasClientSecret { get; init; }

    public IReadOnlyCollection<string> RedirectUris { get; init; } = [];

    public IReadOnlyCollection<string> PostLogoutRedirectUris { get; init; } = [];

    public IReadOnlyCollection<string> AllowedScopes { get; init; } = [];

    public string Status { get; init; } = string.Empty;

    public bool IsActive { get; init; }
}
