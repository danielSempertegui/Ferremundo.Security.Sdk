namespace Ferremundo.Security.Contracts.Sessions.Responses;

public sealed class SecuritySessionResponse
{
    public Guid SessionGuid { get; init; }

    public string SessionIdentifier { get; init; } = string.Empty;

    public string UserName { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public string? ClientId { get; init; }

    public DateTime StartedAtUtc { get; init; }

    public DateTime? ExpiresAtUtc { get; init; }

    public DateTime? RevokedAtUtc { get; init; }

    public string? RevokedReason { get; init; }

    public string? ClientIp { get; init; }

    public string? UserAgent { get; init; }

    public bool IsActive { get; init; }
}
