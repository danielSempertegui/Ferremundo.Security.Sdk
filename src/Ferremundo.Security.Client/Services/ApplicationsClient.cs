using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Applications.Responses;
using Ferremundo.Security.Contracts.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class ApplicationsClient : ExternalRestClientBase, IApplicationsClient
{
    private const string ApplicationsEndpoint = "/api/v1/applications";

    public ApplicationsClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<ApplicationsClient> logger)
        : base(
            httpClient,
            BuildExternalRestClientOptions(options.Value),
            authenticationStrategy,
            correlationProvider,
            logger)
    {
    }

    public async Task<ResponseBase<SecurityApplicationResponse>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await GetAsync<ResponseBase<SecurityApplicationResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(code)}",
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
        => new("The API response could not be deserialized to ResponseBase<SecurityApplicationResponse>.");
}
