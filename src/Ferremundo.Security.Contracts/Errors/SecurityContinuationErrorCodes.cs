namespace Ferremundo.Security.Contracts.Errors;

public static class SecurityContinuationErrorCodes
{
    public const string Invalid = "SECURITY_CONTINUATION_INVALID";
    public const string Expired = "SECURITY_CONTINUATION_EXPIRED";
    public const string Consumed = "SECURITY_CONTINUATION_CONSUMED";
    public const string LogoutInvalid = "SECURITY_LOGOUT_CONTINUATION_INVALID";
    public const string LogoutExpired = "SECURITY_LOGOUT_CONTINUATION_EXPIRED";
    public const string LogoutConsumed = "SECURITY_LOGOUT_CONTINUATION_CONSUMED";
    public const string PostLogoutRedirectUriInvalid = "SECURITY_POST_LOGOUT_REDIRECT_URI_INVALID";
}
