using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Roles.Requests;
using Ferremundo.Security.Contracts.Roles.Responses;

namespace Ferremundo.Security.Client.Services;

public interface IRolesClient
{
    Task<ResponseBase<IReadOnlyCollection<RoleResponse>>> GetAllAsync(
        string applicationCode,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<RoleResponse>> CreateAsync(
        string applicationCode,
        CreateRoleRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<RoleResponse>> GetByCodeAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<RoleResponse>> UpdateAsync(
        string applicationCode,
        string code,
        UpdateRoleRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<RoleResponse>> DeleteAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<RoleResponse>> AssignPermissionAsync(
        string applicationCode,
        string roleCode,
        AssignPermissionToRoleRequest request,
        CancellationToken cancellationToken = default);
}
