using Ferremundo.Security.Contracts.Authentication.Requests;
using Ferremundo.Security.Contracts.Authentication.Responses;
using Ferremundo.Security.Contracts.Common;

namespace Ferremundo.Security.Client.Services;

public interface IAuthenticationClient
{
    Task<ResponseBase<LoginResponse>> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<LogoutResponse>> LogoutAsync(
        CancellationToken cancellationToken = default);
}
