namespace Ferremundo.Security.Contracts;

public static class SecurityAdministrationResponseCodes
{
    public const string ApplicationCreated = "SECURITY_APPLICATION_CREATED";
    public const string PermissionCreated = "SECURITY_PERMISSION_CREATED";
    public const string RoleCreated = "SECURITY_ROLE_CREATED";
    public const string PermissionAssignedToRole = "SECURITY_PERMISSION_ASSIGNED_TO_ROLE";
    public const string UserRegistered = "SECURITY_USER_REGISTERED";
    public const string RoleAssignedToUser = "SECURITY_ROLE_ASSIGNED_TO_USER";
}
