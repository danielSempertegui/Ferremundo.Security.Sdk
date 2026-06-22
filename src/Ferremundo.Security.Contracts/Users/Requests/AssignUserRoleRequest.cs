using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Users.Requests;

public sealed class AssignUserRoleRequest
{
    [Required]
    [StringLength(100)]
    public string ApplicationCode { get; init; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string RoleCode { get; init; } = string.Empty;

    public DateTime? ValidFromUtc { get; init; }

    public DateTime? ValidToUtc { get; init; }
}
