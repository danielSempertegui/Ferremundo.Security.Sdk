namespace Ferremundo.Security.Client.Abstractions.Tokens;

public interface ISecurityClientCredentialsTokenProvider
{
    ValueTask<string> GetAccessTokenAsync(
        string scope,
        CancellationToken cancellationToken = default);
}
