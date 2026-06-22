using Ferremundo.Security.Contracts.Permissions.Responses;

namespace Ferremundo.Security.Contracts.Roles.Responses;

public sealed class RoleResponse
{
    public Guid RoleGuid { get; init; }

    public string ApplicationCode { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string Status { get; init; } = string.Empty;

    public bool IsActive { get; init; }

    public IReadOnlyCollection<PermissionResponse> Permissions { get; init; } = [];
}
