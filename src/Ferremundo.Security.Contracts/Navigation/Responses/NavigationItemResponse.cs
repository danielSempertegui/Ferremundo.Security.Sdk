namespace Ferremundo.Security.Contracts.Navigation.Responses;

public sealed class NavigationItemResponse
{
    public Guid NavigationItemGuid { get; init; }

    public string ApplicationCode { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string? ParentCode { get; init; }

    public string? RequiredPermissionCode { get; init; }

    public string Label { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string ItemType { get; init; } = string.Empty;

    public string? Route { get; init; }

    public string? Icon { get; init; }

    public int DisplayOrder { get; init; }

    public bool IsVisible { get; init; }

    public bool IsActive { get; init; }

    public string Status { get; init; } = string.Empty;

    public IReadOnlyCollection<NavigationItemResponse> Children { get; init; } = [];
}
