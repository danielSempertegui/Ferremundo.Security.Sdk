using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Users.Requests;

public sealed class RegisterAdUserRequest
{
    [Required]
    [StringLength(256)]
    public string UserName { get; init; } = string.Empty;

    [StringLength(100)]
    public string? EmployeeCode { get; init; }
}
