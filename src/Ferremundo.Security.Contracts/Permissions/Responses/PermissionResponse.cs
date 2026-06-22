namespace Ferremundo.Security.Contracts.Permissions.Responses;

public sealed class PermissionResponse
{
    public Guid PermissionGuid { get; init; }

    public string ApplicationCode { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string Status { get; init; } = string.Empty;

    public bool IsActive { get; init; }
}
