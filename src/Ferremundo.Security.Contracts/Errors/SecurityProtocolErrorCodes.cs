namespace Ferremundo.Security.Contracts.Errors;

public static class SecurityProtocolErrorCodes
{
    public const string RequestOriginInvalid = "SECURITY_REQUEST_ORIGIN_INVALID";
    public const string CsrfValidationFailed = "SECURITY_CSRF_VALIDATION_FAILED";
    public const string RateLimited = "SECURITY_RATE_LIMITED";
}
