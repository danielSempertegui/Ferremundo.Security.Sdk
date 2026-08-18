using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Tokens.Requests;
using Ferremundo.Security.Contracts.Tokens.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Ferremundo.Security.Client.Clients.Tokens;

public sealed class SecurityTokenClient : ISecurityTokenClient
{
    private const string TokenEndpoint = "/connect/token";
    private const string ClientCredentialsGrantType = "client_credentials";
    private readonly HttpClient _httpClient;
    private readonly ILogger<SecurityTokenClient> _logger;
    private readonly SecurityClientOptions _options;

    public SecurityTokenClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ILogger<SecurityTokenClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ClientCredentialsTokenResponse> GetClientCredentialsTokenAsync(
        string scope,
        CancellationToken cancellationToken = default)
    {
        return await GetClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest { Scope = scope },
            cancellationToken);
    }

    public async Task<ClientCredentialsTokenResponse> GetClientCredentialsTokenAsync(
        ClientCredentialsTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        var clientId = ResolveClientId(request);
        var clientSecret = ResolveClientSecret(request);

        if (string.IsNullOrWhiteSpace(request.Scope))
        {
            throw new InvalidOperationException("Security token scope is required.");
        }

        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = ClientCredentialsGrantType,
            ["client_id"] = clientId,
            ["client_secret"] = clientSecret,
            ["scope"] = request.Scope
        });

        using var response = await _httpClient.PostAsync(TokenEndpoint, content, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogWarning(
                "Security rejected the client credentials token request. StatusCode: {StatusCode}. Response: {Response}",
                (int)response.StatusCode,
                string.IsNullOrWhiteSpace(responseContent) ? "<empty>" : responseContent);

            throw new InvalidOperationException(
                $"Security rejected the client credentials token request with status code {(int)response.StatusCode}.");
        }

        var token = await response.Content.ReadFromJsonAsync<ClientCredentialsTokenResponse>(
            JsonSerializerOptions.Web,
            cancellationToken);

        if (token is null || string.IsNullOrWhiteSpace(token.AccessToken))
        {
            throw new InvalidOperationException("The Security token response could not be processed.");
        }

        return token;
    }

    private string ResolveClientId(ClientCredentialsTokenRequest request)
    {
        var clientId = string.IsNullOrWhiteSpace(request.ClientId)
            ? _options.ClientId
            : request.ClientId;

        return string.IsNullOrWhiteSpace(clientId)
            ? throw new InvalidOperationException("SecurityClient:ClientId is required to request client credentials tokens.")
            : clientId;
    }

    private string ResolveClientSecret(ClientCredentialsTokenRequest request)
    {
        var clientSecret = string.IsNullOrWhiteSpace(request.ClientSecret)
            ? _options.ClientSecret
            : request.ClientSecret;

        return string.IsNullOrWhiteSpace(clientSecret)
            ? throw new InvalidOperationException("SecurityClient:ClientSecret is required to request client credentials tokens.")
            : clientSecret;
    }
}
