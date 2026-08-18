using Ferremundo.Security.Contracts.Tokens.Requests;
using Ferremundo.Security.Contracts.Tokens.Responses;

namespace Ferremundo.Security.Client.Clients.Tokens;

public interface ISecurityTokenClient
{
    Task<ClientCredentialsTokenResponse> GetClientCredentialsTokenAsync(
        string scope,
        CancellationToken cancellationToken = default);

    Task<ClientCredentialsTokenResponse> GetClientCredentialsTokenAsync(
        ClientCredentialsTokenRequest request,
        CancellationToken cancellationToken = default);
}
