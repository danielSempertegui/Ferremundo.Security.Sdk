namespace Ferremundo.Security.Contracts.OAuthScopes.Responses;

public sealed class OAuthScopeResponse
{
    public Guid ScopeGuid { get; init; }

    public string Name { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string Status { get; init; } = string.Empty;

    public bool IsActive { get; init; }
}
