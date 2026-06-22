using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Users.Requests;

public sealed class UpdateSecurityUserRequest
{
    [Required]
    [StringLength(200)]
    public string DisplayName { get; init; } = string.Empty;

    [StringLength(320)]
    public string? Email { get; init; }

    [StringLength(50)]
    public string? EmployeeCode { get; init; }

    public bool IsActive { get; init; } = true;
}
