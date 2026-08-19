namespace Ferremundo.Security.Contracts.Errors;

public static class SecurityProtocolErrorMessages
{
    public const string RequestOriginInvalid = "The request origin is not allowed.";
    public const string CsrfValidationFailed = "The anti-forgery token is missing or invalid.";
    public const string RateLimited = "Too many login attempts. Please try again later.";
    public const string Unexpected = "An unexpected error occurred.";
}
