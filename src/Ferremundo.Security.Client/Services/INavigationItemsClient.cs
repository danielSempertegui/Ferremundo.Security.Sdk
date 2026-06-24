using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Navigation.Requests;
using Ferremundo.Security.Contracts.Navigation.Responses;

namespace Ferremundo.Security.Client.Services;

public interface INavigationItemsClient
{
    Task<ResponseBase<ApplicationNavigationResponse>> GetTreeAsync(
        string applicationCode,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<ApplicationNavigationResponse>> GetMyNavigationAsync(
        string applicationCode,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<NavigationItemResponse>> CreateAsync(
        string applicationCode,
        CreateNavigationItemRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<NavigationItemResponse>> GetByCodeAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<NavigationItemResponse>> UpdateAsync(
        string applicationCode,
        string code,
        UpdateNavigationItemRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<NavigationItemResponse>> DeleteAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default);
}
