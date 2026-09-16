using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.OAuthClients.Requests;

public sealed class UpdateOAuthClientStatusRequest
{
    [Required]
    public bool? IsActive { get; init; }
}
