namespace Ferremundo.Security.Contracts.Authentication.Responses;

public sealed class LogoutContinuationContextResponse
{
    public string? ApplicationName { get; init; }

    public string? LogoUrl { get; init; }

    public string? Description { get; init; }

    public string? OwnerName { get; init; }

    public string? SupportUrl { get; init; }

    public string? Environment { get; init; }

    public bool CanReturnToApplication { get; init; }

    public DateTime ExpiresAtUtc { get; init; }
}
