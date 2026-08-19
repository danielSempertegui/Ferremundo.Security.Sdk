namespace Ferremundo.Security.Contracts.Authentication.Responses;

public sealed class AuthenticationContinuationContextResponse
{
    public string ApplicationName { get; init; } = string.Empty;

    public string? LogoUrl { get; init; }

    public string? Description { get; init; }

    public string? OwnerName { get; init; }

    public string? SupportUrl { get; init; }

    public string? PrivacyUrl { get; init; }

    public string? Environment { get; init; }

    public DateTime ExpiresAtUtc { get; init; }
}
