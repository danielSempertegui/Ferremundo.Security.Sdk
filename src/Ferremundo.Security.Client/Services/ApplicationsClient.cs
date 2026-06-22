using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Applications.Requests;
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

    public async Task<ResponseBase<IReadOnlyCollection<SecurityApplicationResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<ResponseBase<IReadOnlyCollection<SecurityApplicationResponse>>>(
                   ApplicationsEndpoint,
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<IReadOnlyCollection<SecurityApplicationResponse>>.");
    }

    public async Task<ResponseBase<SecurityApplicationResponse>> CreateAsync(
        CreateSecurityApplicationRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await PostAsync<CreateSecurityApplicationRequest, ResponseBase<SecurityApplicationResponse>>(
                   ApplicationsEndpoint,
                   request,
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<SecurityApplicationResponse>> UpdateAsync(
        string code,
        UpdateSecurityApplicationRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<UpdateSecurityApplicationRequest, ResponseBase<SecurityApplicationResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(code)}",
                   request,
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<SecurityApplicationResponse>> DeleteAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await DeleteAsync<ResponseBase<SecurityApplicationResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(code)}",
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
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
