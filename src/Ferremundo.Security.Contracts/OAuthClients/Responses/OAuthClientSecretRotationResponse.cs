namespace Ferremundo.Security.Contracts.OAuthClients.Responses;

public sealed class OAuthClientSecretRotationResponse
{
    public string ClientId { get; init; } = string.Empty;

    public string ClientSecret { get; init; } = string.Empty;
}
