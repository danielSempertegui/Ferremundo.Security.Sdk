namespace Ferremundo.Security.Client.Abstractions;

public interface ISecurityAccessTokenProvider
{
    ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
