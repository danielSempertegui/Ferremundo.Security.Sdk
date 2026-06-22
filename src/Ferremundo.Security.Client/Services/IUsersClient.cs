using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Users.Requests;
using Ferremundo.Security.Contracts.Users.Responses;

namespace Ferremundo.Security.Client.Services;

public interface IUsersClient
{
    Task<ResponseBase<IReadOnlyCollection<SecurityUserResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityUserResponse>> RegisterAdUserAsync(
        RegisterAdUserRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityUserResponse>> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityUserResponse>> UpdateAsync(
        string userName,
        UpdateSecurityUserRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityUserResponse>> DeleteAsync(
        string userName,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityUserResponse>> AssignRoleAsync(
        string userName,
        AssignUserRoleRequest request,
        CancellationToken cancellationToken = default);
}
