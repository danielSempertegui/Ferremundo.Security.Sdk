using Ferremundo.Security.Contracts.Applications.Requests;
using Ferremundo.Security.Contracts.Applications.Responses;
using Ferremundo.Security.Contracts.Common;

namespace Ferremundo.Security.Client.Services;

public interface IApplicationsClient
{
    Task<ResponseBase<SecurityApplicationResponse>> CreateAsync(
        CreateSecurityApplicationRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityApplicationResponse>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);
}
