using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Me.Responses;

namespace Ferremundo.Security.Client.Services;

public interface IMeClient
{
    Task<ResponseBase<MeContextResponse>> GetContextAsync(
        Guid applicationId,
        CancellationToken cancellationToken = default);
}
