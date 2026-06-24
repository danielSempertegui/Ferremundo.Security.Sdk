namespace Ferremundo.Security.Contracts;

public static class SecurityAdministrationResponseCodes
{
    public const string ApplicationCreated = "SECURITY_APPLICATION_CREATED";
    public const string ApplicationUpdated = "SECURITY_APPLICATION_UPDATED";
    public const string ApplicationDeleted = "SECURITY_APPLICATION_DELETED";
    public const string PermissionCreated = "SECURITY_PERMISSION_CREATED";
    public const string PermissionUpdated = "SECURITY_PERMISSION_UPDATED";
    public const string PermissionDeleted = "SECURITY_PERMISSION_DELETED";
    public const string RoleCreated = "SECURITY_ROLE_CREATED";
    public const string RoleUpdated = "SECURITY_ROLE_UPDATED";
    public const string RoleDeleted = "SECURITY_ROLE_DELETED";
    public const string PermissionAssignedToRole = "SECURITY_PERMISSION_ASSIGNED_TO_ROLE";
    public const string UserRegistered = "SECURITY_USER_REGISTERED";
    public const string UserUpdated = "SECURITY_USER_UPDATED";
    public const string UserDeleted = "SECURITY_USER_DELETED";
    public const string RoleAssignedToUser = "SECURITY_ROLE_ASSIGNED_TO_USER";
    public const string OAuthScopeCreated = "SECURITY_OAUTH_SCOPE_CREATED";
    public const string OAuthScopeUpdated = "SECURITY_OAUTH_SCOPE_UPDATED";
    public const string OAuthScopeDeleted = "SECURITY_OAUTH_SCOPE_DELETED";
    public const string OAuthClientCreated = "SECURITY_OAUTH_CLIENT_CREATED";
    public const string OAuthClientUpdated = "SECURITY_OAUTH_CLIENT_UPDATED";
    public const string OAuthClientDeleted = "SECURITY_OAUTH_CLIENT_DELETED";
    public const string PermissionAssignedToOAuthClient = "SECURITY_PERMISSION_ASSIGNED_TO_OAUTH_CLIENT";
    public const string SessionRevoked = "SECURITY_SESSION_REVOKED";
    public const string NavigationItemCreated = "SECURITY_NAVIGATION_ITEM_CREATED";
    public const string NavigationItemUpdated = "SECURITY_NAVIGATION_ITEM_UPDATED";
    public const string NavigationItemDeleted = "SECURITY_NAVIGATION_ITEM_DELETED";
}
