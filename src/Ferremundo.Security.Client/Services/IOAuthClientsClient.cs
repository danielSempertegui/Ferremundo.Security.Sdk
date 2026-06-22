using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.OAuthClients.Requests;
using Ferremundo.Security.Contracts.OAuthClients.Responses;

namespace Ferremundo.Security.Client.Services;

public interface IOAuthClientsClient
{
    Task<ResponseBase<IReadOnlyCollection<OAuthClientResponse>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> CreateAsync(
        CreateOAuthClientRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> GetByClientIdAsync(string clientId, CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> UpdateAsync(
        string clientId,
        UpdateOAuthClientRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> DeleteAsync(string clientId, CancellationToken cancellationToken = default);
}
