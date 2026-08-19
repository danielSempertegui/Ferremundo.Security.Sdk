namespace Ferremundo.Security.Contracts.Errors;

public static class SecurityOAuthErrorCodes
{
    public const string ClientAlreadyExists = "SECURITY_OAUTH_CLIENT_ALREADY_EXISTS";
    public const string ClientNotFound = "SECURITY_OAUTH_CLIENT_NOT_FOUND";
    public const string ClientInactive = "SECURITY_OAUTH_CLIENT_INACTIVE";
    public const string InvalidClientType = "SECURITY_INVALID_OAUTH_CLIENT_TYPE";
    public const string ClientSecretRequired = "SECURITY_OAUTH_CLIENT_SECRET_REQUIRED";
    public const string ClientApplicationRequired = "SECURITY_OAUTH_CLIENT_APPLICATION_REQUIRED";
    public const string ClientApplicationInvalid = "SECURITY_OAUTH_CLIENT_APPLICATION_INVALID";
    public const string ClientIdRequired = "SECURITY_CLIENT_ID_REQUIRED";
    public const string ScopeAlreadyExists = "SECURITY_OAUTH_SCOPE_ALREADY_EXISTS";
    public const string ScopeNotFound = "SECURITY_OAUTH_SCOPE_NOT_FOUND";
}
