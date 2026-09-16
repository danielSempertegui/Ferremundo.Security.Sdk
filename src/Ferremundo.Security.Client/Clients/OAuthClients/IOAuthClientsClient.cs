using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.OAuthClients.Requests;
using Ferremundo.Security.Contracts.OAuthClients.Responses;

namespace Ferremundo.Security.Client.Clients.OAuthClients;

public interface IOAuthClientsClient
{
    Task<ResponseBase<IReadOnlyCollection<OAuthClientResponse>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientCreationResponse>> CreateAsync(
        CreateOAuthClientRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> GetByClientIdAsync(string clientId, CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> UpdateAsync(
        string clientId,
        UpdateOAuthClientRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> UpdateStatusAsync(
        string clientId,
        UpdateOAuthClientStatusRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> DeleteAsync(string clientId, CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientSecretRotationResponse>> RotateSecretAsync(
        string clientId,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> AssignPermissionAsync(
        string clientId,
        AssignPermissionToOAuthClientRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> RemovePermissionAsync(
        string clientId,
        string applicationCode,
        string permissionCode,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> AssignScopeAsync(
        string clientId,
        string scopeName,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<OAuthClientResponse>> RemoveScopeAsync(
        string clientId,
        string scopeName,
        CancellationToken cancellationToken = default);
}
