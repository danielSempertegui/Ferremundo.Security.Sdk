namespace Ferremundo.Security.Contracts.Errors;

public static class SecurityUserErrorCodes
{
    public const string AlreadyExists = "SECURITY_USER_ALREADY_EXISTS";
    public const string NotFound = "SECURITY_USER_NOT_FOUND";
    public const string Inactive = "SECURITY_USER_INACTIVE";
    public const string UserNameRequired = "SECURITY_USER_NAME_REQUIRED";
    public const string IdentityProviderNotFound = "SECURITY_IDENTITY_PROVIDER_NOT_FOUND";
    public const string LdapUserNotFound = "SECURITY_LDAP_USER_NOT_FOUND";
}
