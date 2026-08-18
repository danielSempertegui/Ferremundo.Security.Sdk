using Ferremundo.Security.Authentication.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Authentication.Authorization;

public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IOptionsMonitor<FerremundoSecurityAuthenticationOptions> _options;

    public PermissionAuthorizationHandler(IOptionsMonitor<FerremundoSecurityAuthenticationOptions> options)
    {
        _options = options;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var permissionClaimType = _options.CurrentValue.PermissionClaimType;

        var hasPermission = context.User.Claims.Any(claim =>
            string.Equals(claim.Type, permissionClaimType, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(claim.Value, requirement.Permission, StringComparison.OrdinalIgnoreCase));

        if (hasPermission)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
