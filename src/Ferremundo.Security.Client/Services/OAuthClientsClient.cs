using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.OAuthClients.Requests;
using Ferremundo.Security.Contracts.OAuthClients.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class OAuthClientsClient : ExternalRestClientBase, IOAuthClientsClient
{
    private const string OAuthClientsEndpoint = "/api/v1/oauth/clients";

    public OAuthClientsClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<OAuthClientsClient> logger)
        : base(httpClient, BuildExternalRestClientOptions(options.Value), authenticationStrategy, correlationProvider, logger)
    {
    }

    public async Task<ResponseBase<IReadOnlyCollection<OAuthClientResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        => await GetAsync<ResponseBase<IReadOnlyCollection<OAuthClientResponse>>>(OAuthClientsEndpoint, cancellationToken)
           ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<IReadOnlyCollection<OAuthClientResponse>>.");

    public async Task<ResponseBase<OAuthClientResponse>> CreateAsync(CreateOAuthClientRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return await PostAsync<CreateOAuthClientRequest, ResponseBase<OAuthClientResponse>>(OAuthClientsEndpoint, request, cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<OAuthClientResponse>> GetByClientIdAsync(string clientId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        return await GetAsync<ResponseBase<OAuthClientResponse>>($"{OAuthClientsEndpoint}/{Uri.EscapeDataString(clientId)}", cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<OAuthClientResponse>> UpdateAsync(string clientId, UpdateOAuthClientRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentNullException.ThrowIfNull(request);
        return await PutAsync<UpdateOAuthClientRequest, ResponseBase<OAuthClientResponse>>($"{OAuthClientsEndpoint}/{Uri.EscapeDataString(clientId)}", request, cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<OAuthClientResponse>> DeleteAsync(string clientId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        return await DeleteAsync<ResponseBase<OAuthClientResponse>>($"{OAuthClientsEndpoint}/{Uri.EscapeDataString(clientId)}", cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<OAuthClientResponse>> AssignPermissionAsync(
        string clientId,
        AssignPermissionToOAuthClientRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<AssignPermissionToOAuthClientRequest, ResponseBase<OAuthClientResponse>>(
                   $"{OAuthClientsEndpoint}/{Uri.EscapeDataString(clientId)}/permissions",
                   request,
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    private static ExternalRestClientOptions BuildExternalRestClientOptions(SecurityClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        return new ExternalRestClientOptions
        {
            ServiceName = "Ferremundo.Security",
            BaseUrl = options.BaseUrl,
            TimeoutSeconds = options.TimeoutSeconds,
            RetryCount = options.RetryCount,
            RetryDelayMilliseconds = options.RetryDelayMilliseconds,
            CorrelationHeaderName = options.CorrelationHeaderName
        };
    }

    private static InvalidOperationException CreateEmptyResponseException()
        => new("The API response could not be deserialized to ResponseBase<OAuthClientResponse>.");
}
