using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Sessions.Requests;
using Ferremundo.Security.Contracts.Sessions.Responses;

namespace Ferremundo.Security.Client.Services;

public interface ISecuritySessionsClient
{
    Task<ResponseBase<IReadOnlyCollection<SecuritySessionResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<ResponseBase<IReadOnlyCollection<SecuritySessionResponse>>> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecuritySessionResponse>> RevokeAsync(
        Guid sessionGuid,
        RevokeSecuritySessionRequest request,
        CancellationToken cancellationToken = default);
}
