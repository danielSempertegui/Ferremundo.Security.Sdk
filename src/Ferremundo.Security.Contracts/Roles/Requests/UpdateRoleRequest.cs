using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Roles.Requests;

public sealed class UpdateRoleRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }

    public bool IsActive { get; init; } = true;
}
