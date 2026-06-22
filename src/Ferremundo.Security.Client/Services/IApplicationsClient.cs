using Ferremundo.Security.Contracts.Applications.Responses;
using Ferremundo.Security.Contracts.Common;

namespace Ferremundo.Security.Client.Services;

public interface IApplicationsClient
{
    Task<ResponseBase<SecurityApplicationResponse>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);
}
