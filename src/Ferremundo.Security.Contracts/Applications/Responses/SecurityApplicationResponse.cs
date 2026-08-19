namespace Ferremundo.Security.Contracts.Applications.Responses;

public sealed class SecurityApplicationResponse
{
    public Guid ApplicationGuid { get; init; }

    public string Code { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string? LogoUrl { get; init; }

    public string? OwnerName { get; init; }

    public string? SupportUrl { get; init; }

    public string? PrivacyUrl { get; init; }

    public string Status { get; init; } = string.Empty;

    public bool IsActive { get; init; }
}
