using Ferremundo.Security.Contracts.Roles.Responses;

namespace Ferremundo.Security.Contracts.Users.Responses;

public sealed class SecurityUserResponse
{
    public Guid UserGuid { get; init; }

    public string UserName { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public string? Email { get; init; }

    public string? EmployeeCode { get; init; }

    public string Status { get; init; } = string.Empty;

    public bool IsActive { get; init; }

    public IReadOnlyCollection<RoleResponse> Roles { get; init; } = [];
}
