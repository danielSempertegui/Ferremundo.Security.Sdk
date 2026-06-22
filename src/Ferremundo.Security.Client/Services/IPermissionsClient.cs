using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Permissions.Requests;
using Ferremundo.Security.Contracts.Permissions.Responses;

namespace Ferremundo.Security.Client.Services;

public interface IPermissionsClient
{
    Task<ResponseBase<PermissionResponse>> CreateAsync(
        string applicationCode,
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<PermissionResponse>> GetByCodeAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default);
}
