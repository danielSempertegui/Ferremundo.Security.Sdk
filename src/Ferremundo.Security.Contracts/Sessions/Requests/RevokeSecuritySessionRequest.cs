using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.Sessions.Requests;

public sealed class RevokeSecuritySessionRequest
{
    [StringLength(1000)]
    public string? Reason { get; init; }
}
