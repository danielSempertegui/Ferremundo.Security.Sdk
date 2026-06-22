using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Permissions.Requests;

public sealed class CreatePermissionRequest
{
    [Required]
    [StringLength(150)]
    public string Code { get; init; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }
}
