using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Applications.Requests;

public sealed class CreateSecurityApplicationRequest
{
    [Required]
    [StringLength(100)]
    public string Code { get; init; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }
}
