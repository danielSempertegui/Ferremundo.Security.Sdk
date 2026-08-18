namespace Ferremundo.Security.Client.Abstractions.Authentication;

public interface ISecurityAccessTokenProvider
{
    ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
