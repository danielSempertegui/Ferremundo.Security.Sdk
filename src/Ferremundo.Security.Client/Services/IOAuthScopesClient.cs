using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.OAuthScopes.Requests;
using Ferremundo.Security.Contracts.OAuthScopes.Responses;

namespace Ferremundo.Security.Client.Services;

public interface IOAuthScopesClient
{
    Task<ResponseBase<IReadOnlyCollection<OAuthScopeResponse>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthScopeResponse>> CreateAsync(
        CreateOAuthScopeRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthScopeResponse>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthScopeResponse>> UpdateAsync(
        string name,
        UpdateOAuthScopeRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthScopeResponse>> DeleteAsync(string name, CancellationToken cancellationToken = default);
}
