using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.OAuthClients.Requests;

public sealed class UpdateOAuthClientRequest
{
    [Required]
    [StringLength(200)]
    public string DisplayName { get; init; } = string.Empty;

    public string? ClientSecret { get; init; }

    public bool AllowRefreshTokens { get; init; }

    public bool AllowMultipleActiveUserSessions { get; init; } = true;

    [Range(5, 43200)]
    public int UserSessionLifetimeMinutes { get; init; } = 480;

    public IReadOnlyCollection<string> RedirectUris { get; init; } = [];

    public IReadOnlyCollection<string> PostLogoutRedirectUris { get; init; } = [];

    public IReadOnlyCollection<string> AllowedScopes { get; init; } = [];

    public bool IsActive { get; init; } = true;
}
