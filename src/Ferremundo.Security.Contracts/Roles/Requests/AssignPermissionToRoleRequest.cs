using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Roles.Requests;

public sealed class AssignPermissionToRoleRequest
{
    [Required]
    [StringLength(150)]
    public string PermissionCode { get; init; } = string.Empty;
}
