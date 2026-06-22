using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.OAuthScopes.Requests;
using Ferremundo.Security.Contracts.OAuthScopes.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class OAuthScopesClient : ExternalRestClientBase, IOAuthScopesClient
{
    private const string OAuthScopesEndpoint = "/api/v1/oauth/scopes";

    public OAuthScopesClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<OAuthScopesClient> logger)
        : base(httpClient, BuildExternalRestClientOptions(options.Value), authenticationStrategy, correlationProvider, logger)
    {
    }

    public async Task<ResponseBase<IReadOnlyCollection<OAuthScopeResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        => await GetAsync<ResponseBase<IReadOnlyCollection<OAuthScopeResponse>>>(OAuthScopesEndpoint, cancellationToken)
           ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<IReadOnlyCollection<OAuthScopeResponse>>.");

    public async Task<ResponseBase<OAuthScopeResponse>> CreateAsync(CreateOAuthScopeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return await PostAsync<CreateOAuthScopeRequest, ResponseBase<OAuthScopeResponse>>(OAuthScopesEndpoint, request, cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<OAuthScopeResponse>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return await GetAsync<ResponseBase<OAuthScopeResponse>>($"{OAuthScopesEndpoint}/{Uri.EscapeDataString(name)}", cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<OAuthScopeResponse>> UpdateAsync(string name, UpdateOAuthScopeRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(request);
        return await PutAsync<UpdateOAuthScopeRequest, ResponseBase<OAuthScopeResponse>>($"{OAuthScopesEndpoint}/{Uri.EscapeDataString(name)}", request, cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<OAuthScopeResponse>> DeleteAsync(string name, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return await DeleteAsync<ResponseBase<OAuthScopeResponse>>($"{OAuthScopesEndpoint}/{Uri.EscapeDataString(name)}", cancellationToken)
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
        => new("The API response could not be deserialized to ResponseBase<OAuthScopeResponse>.");
}
