using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.OAuthClients.Requests;

public sealed class CreateOAuthClientRequest
{
    [Required]
    [StringLength(150)]
    public string ClientId { get; init; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string DisplayName { get; init; } = string.Empty;

    [Required]
    [RegularExpression("web|api|worker", ErrorMessage = "ClientType must be web, api, or worker.")]
    public string ClientType { get; init; } = string.Empty;

    public string? ClientSecret { get; init; }

    public bool AllowRefreshTokens { get; init; }

    public bool AllowTokenIntrospection { get; init; }

    public bool AllowMultipleActiveUserSessions { get; init; } = true;

    [Range(5, 43200)]
    public int UserSessionLifetimeMinutes { get; init; } = 480;

    [Range(1, 1440)]
    public int? AccessTokenLifetimeMinutes { get; init; }

    [Range(5, 43200)]
    public int? RefreshTokenLifetimeMinutes { get; init; }

    [Range(1, 60)]
    public int? AuthorizationCodeLifetimeMinutes { get; init; }

    public IReadOnlyCollection<string> RedirectUris { get; init; } = [];

    public IReadOnlyCollection<string> PostLogoutRedirectUris { get; init; } = [];

    public IReadOnlyCollection<string> AllowedScopes { get; init; } = [];
}
