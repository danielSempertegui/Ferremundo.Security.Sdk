namespace Ferremundo.Security.Contracts.OAuthClients.Responses;

public sealed class OAuthClientCreationResponse
{
    public OAuthClientResponse Client { get; init; } = new();

    public string? ClientSecret { get; init; }
}
