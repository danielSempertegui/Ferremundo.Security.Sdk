using Ferremundo.Security.Contracts.Applications.Responses;
using Ferremundo.Security.Contracts.Navigation.Responses;
using Ferremundo.Security.Contracts.Permissions.Responses;
using Ferremundo.Security.Contracts.Roles.Responses;
using Ferremundo.Security.Contracts.Users.Responses;

namespace Ferremundo.Security.Contracts.Me.Responses;

public sealed class MeContextResponse
{
    public SecurityUserResponse User { get; init; } = new();

    public SecurityApplicationResponse Application { get; init; } = new();

    public IReadOnlyCollection<RoleResponse> Roles { get; init; } = [];

    public IReadOnlyCollection<PermissionResponse> Permissions { get; init; } = [];

    public IReadOnlyCollection<NavigationItemResponse> NavigationItems { get; init; } = [];
}
