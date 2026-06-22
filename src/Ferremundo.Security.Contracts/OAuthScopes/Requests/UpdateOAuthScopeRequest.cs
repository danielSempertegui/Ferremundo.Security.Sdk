using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.OAuthScopes.Requests;

public sealed class UpdateOAuthScopeRequest
{
    [Required]
    [StringLength(200)]
    public string DisplayName { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }

    public bool IsActive { get; init; } = true;
}
