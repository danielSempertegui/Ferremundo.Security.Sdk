using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Sessions.Requests;
using Ferremundo.Security.Contracts.Sessions.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class SecuritySessionsClient : ExternalRestClientBase, ISecuritySessionsClient
{
    private const string SessionsEndpoint = "/api/v1/sessions";

    public SecuritySessionsClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<SecuritySessionsClient> logger)
        : base(httpClient, BuildExternalRestClientOptions(options.Value), authenticationStrategy, correlationProvider, logger)
    {
    }

    public async Task<ResponseBase<IReadOnlyCollection<SecuritySessionResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<ResponseBase<IReadOnlyCollection<SecuritySessionResponse>>>(
                   SessionsEndpoint,
                   cancellationToken)
               ?? throw CreateCollectionEmptyResponseException();
    }

    public async Task<ResponseBase<IReadOnlyCollection<SecuritySessionResponse>>> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        return await GetAsync<ResponseBase<IReadOnlyCollection<SecuritySessionResponse>>>(
                   $"{SessionsEndpoint}/users/{Uri.EscapeDataString(userName)}",
                   cancellationToken)
               ?? throw CreateCollectionEmptyResponseException();
    }

    public async Task<ResponseBase<SecuritySessionResponse>> RevokeAsync(
        Guid sessionGuid,
        RevokeSecuritySessionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<RevokeSecuritySessionRequest, ResponseBase<SecuritySessionResponse>>(
                   $"{SessionsEndpoint}/{sessionGuid}/revoke",
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

    private static InvalidOperationException CreateCollectionEmptyResponseException()
        => new("The API response could not be deserialized to ResponseBase<IReadOnlyCollection<SecuritySessionResponse>>.");

    private static InvalidOperationException CreateEmptyResponseException()
        => new("The API response could not be deserialized to ResponseBase<SecuritySessionResponse>.");
}
