using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Users.Requests;
using Ferremundo.Security.Contracts.Users.Responses;

namespace Ferremundo.Security.Client.Services;

public interface IUsersClient
{
    Task<ResponseBase<SecurityUserResponse>> RegisterAdUserAsync(
        RegisterAdUserRequest request,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityUserResponse>> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default);

    Task<ResponseBase<SecurityUserResponse>> AssignRoleAsync(
        string userName,
        AssignUserRoleRequest request,
        CancellationToken cancellationToken = default);
}
