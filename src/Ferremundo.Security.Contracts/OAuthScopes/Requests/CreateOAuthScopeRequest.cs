using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.OAuthScopes.Requests;

public sealed class CreateOAuthScopeRequest
{
    [Required]
    [StringLength(150)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string DisplayName { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }
}
