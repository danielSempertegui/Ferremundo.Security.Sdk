using System.ComponentModel.DataAnnotations;

namespace Ferremundo.Security.Contracts.OAuthClients.Requests;

public sealed class AssignPermissionToOAuthClientRequest
{
    [Required]
    [StringLength(100)]
    public string ApplicationCode { get; init; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string PermissionCode { get; init; } = string.Empty;
}
