using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Me.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class MeClient : ExternalRestClientBase, IMeClient
{
    private const string MeEndpoint = "/api/v1/me";

    public MeClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<MeClient> logger)
        : base(
            httpClient,
            BuildExternalRestClientOptions(options.Value),
            authenticationStrategy,
            correlationProvider,
            logger)
    {
    }

    public async Task<ResponseBase<MeContextResponse>> GetContextAsync(
        Guid applicationId,
        CancellationToken cancellationToken = default)
    {
        if (applicationId == Guid.Empty)
        {
            throw new ArgumentException("The application id is required.", nameof(applicationId));
        }

        return await GetAsync<ResponseBase<MeContextResponse>>(
                   $"{MeEndpoint}/context?applicationId={Uri.EscapeDataString(applicationId.ToString())}",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<MeContextResponse>.");
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
}
