using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Permissions.Requests;
using Ferremundo.Security.Contracts.Permissions.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class PermissionsClient : ExternalRestClientBase, IPermissionsClient
{
    private const string ApplicationsEndpoint = "/api/v1/applications";

    public PermissionsClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<PermissionsClient> logger)
        : base(
            httpClient,
            BuildExternalRestClientOptions(options.Value),
            authenticationStrategy,
            correlationProvider,
            logger)
    {
    }

    public async Task<ResponseBase<IReadOnlyCollection<PermissionResponse>>> GetAllAsync(
        string applicationCode,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);

        return await GetAsync<ResponseBase<IReadOnlyCollection<PermissionResponse>>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/permissions",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<IReadOnlyCollection<PermissionResponse>>.");
    }

    public async Task<ResponseBase<PermissionResponse>> CreateAsync(
        string applicationCode,
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentNullException.ThrowIfNull(request);

        return await PostAsync<CreatePermissionRequest, ResponseBase<PermissionResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/permissions",
                   request,
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<PermissionResponse>.");
    }

    public async Task<ResponseBase<PermissionResponse>> UpdateAsync(
        string applicationCode,
        string code,
        UpdatePermissionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<UpdatePermissionRequest, ResponseBase<PermissionResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/permissions/{Uri.EscapeDataString(code)}",
                   request,
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<PermissionResponse>.");
    }

    public async Task<ResponseBase<PermissionResponse>> DeleteAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await DeleteAsync<ResponseBase<PermissionResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/permissions/{Uri.EscapeDataString(code)}",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<PermissionResponse>.");
    }

    public async Task<ResponseBase<PermissionResponse>> GetByCodeAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await GetAsync<ResponseBase<PermissionResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/permissions/{Uri.EscapeDataString(code)}",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<PermissionResponse>.");
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
