using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Navigation.Requests;

public sealed class UpdateNavigationItemRequest
{
    [StringLength(150)]
    public string? ParentCode { get; init; }

    [StringLength(150)]
    public string? RequiredPermissionCode { get; init; }

    [Required]
    [StringLength(200)]
    public string Label { get; init; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; init; }

    [Required]
    [StringLength(50)]
    public string ItemType { get; init; } = string.Empty;

    [StringLength(500)]
    public string? Route { get; init; }

    [StringLength(100)]
    public string? Icon { get; init; }

    public int DisplayOrder { get; init; }

    public bool IsVisible { get; init; } = true;

    public bool IsActive { get; init; } = true;
}
