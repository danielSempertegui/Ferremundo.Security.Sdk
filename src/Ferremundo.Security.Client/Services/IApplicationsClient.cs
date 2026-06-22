using Ferremundo.Security.Contracts.Applications.Requests;
using Ferremundo.Security.Contracts.Applications.Responses;
using Ferremundo.Security.Contracts.Common;

namespace Ferremundo.Security.Client.Services;

public interface IApplicationsClient
{
    Task<ResponseBase<IReadOnlyCollection<SecurityApplicationResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityApplicationResponse>> CreateAsync(
        CreateSecurityApplicationRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityApplicationResponse>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityApplicationResponse>> UpdateAsync(
        string code,
        UpdateSecurityApplicationRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityApplicationResponse>> DeleteAsync(
        string code,
        CancellationToken cancellationToken = default);
}
