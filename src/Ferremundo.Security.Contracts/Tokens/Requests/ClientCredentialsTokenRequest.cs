namespace Ferremundo.Security.Contracts.Tokens.Requests;

public sealed class ClientCredentialsTokenRequest
{
    public string Scope { get; init; } = string.Empty;

    public string? ClientId { get; init; }

    public string? ClientSecret { get; init; }
}
