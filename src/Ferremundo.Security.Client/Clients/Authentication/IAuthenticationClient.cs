using Ferremundo.Security.Contracts.Authentication.Requests;
using Ferremundo.Security.Contracts.Authentication.Responses;
using Ferremundo.Security.Contracts.Common;

namespace Ferremundo.Security.Client.Clients.Authentication;

public interface IAuthenticationClient
{
    Task<ResponseBase<AuthenticationContinuationContextResponse>> GetContinuationContextAsync(
        string continuationId,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<LoginResponse>> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<LogoutContinuationContextResponse>> GetLogoutContinuationContextAsync(
        string continuationId,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<LogoutResponse>> LogoutAsync(
        CancellationToken cancellationToken = default);
}
