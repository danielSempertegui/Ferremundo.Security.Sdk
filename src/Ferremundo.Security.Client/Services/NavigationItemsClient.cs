using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Navigation.Requests;
using Ferremundo.Security.Contracts.Navigation.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class NavigationItemsClient : ExternalRestClientBase, INavigationItemsClient
{
    private const string ApplicationsEndpoint = "/api/v1/applications";
    private const string MeEndpoint = "/api/v1/me";

    public NavigationItemsClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<NavigationItemsClient> logger)
        : base(
            httpClient,
            BuildExternalRestClientOptions(options.Value),
            authenticationStrategy,
            correlationProvider,
            logger)
    {
    }

    public async Task<ResponseBase<ApplicationNavigationResponse>> GetTreeAsync(
        string applicationCode,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);

        return await GetAsync<ResponseBase<ApplicationNavigationResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/navigation-items",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<ApplicationNavigationResponse>.");
    }

    public async Task<ResponseBase<ApplicationNavigationResponse>> GetMyNavigationAsync(
        string applicationCode,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);

        return await GetAsync<ResponseBase<ApplicationNavigationResponse>>(
                   $"{MeEndpoint}/applications/{Uri.EscapeDataString(applicationCode)}/navigation",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<ApplicationNavigationResponse>.");
    }

    public async Task<ResponseBase<NavigationItemResponse>> CreateAsync(
        string applicationCode,
        CreateNavigationItemRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentNullException.ThrowIfNull(request);

        return await PostAsync<CreateNavigationItemRequest, ResponseBase<NavigationItemResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/navigation-items",
                   request,
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<NavigationItemResponse>.");
    }

    public async Task<ResponseBase<NavigationItemResponse>> GetByCodeAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await GetAsync<ResponseBase<NavigationItemResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/navigation-items/{Uri.EscapeDataString(code)}",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<NavigationItemResponse>.");
    }

    public async Task<ResponseBase<NavigationItemResponse>> UpdateAsync(
        string applicationCode,
        string code,
        UpdateNavigationItemRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<UpdateNavigationItemRequest, ResponseBase<NavigationItemResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/navigation-items/{Uri.EscapeDataString(code)}",
                   request,
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<NavigationItemResponse>.");
    }

    public async Task<ResponseBase<NavigationItemResponse>> DeleteAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await DeleteAsync<ResponseBase<NavigationItemResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/navigation-items/{Uri.EscapeDataString(code)}",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<NavigationItemResponse>.");
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
