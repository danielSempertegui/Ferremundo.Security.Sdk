namespace Ferremundo.Security.Contracts.Navigation.Responses;

public sealed class ApplicationNavigationResponse
{
    public string ApplicationCode { get; init; } = string.Empty;

    public IReadOnlyCollection<NavigationItemResponse> Items { get; init; } = [];
}
