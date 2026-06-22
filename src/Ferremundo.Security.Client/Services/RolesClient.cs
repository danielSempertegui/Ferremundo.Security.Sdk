using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Roles.Requests;
using Ferremundo.Security.Contracts.Roles.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class RolesClient : ExternalRestClientBase, IRolesClient
{
    private const string ApplicationsEndpoint = "/api/v1/applications";

    public RolesClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<RolesClient> logger)
        : base(
            httpClient,
            BuildExternalRestClientOptions(options.Value),
            authenticationStrategy,
            correlationProvider,
            logger)
    {
    }

    public async Task<ResponseBase<IReadOnlyCollection<RoleResponse>>> GetAllAsync(
        string applicationCode,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);

        return await GetAsync<ResponseBase<IReadOnlyCollection<RoleResponse>>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/roles",
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<IReadOnlyCollection<RoleResponse>>.");
    }

    public async Task<ResponseBase<RoleResponse>> CreateAsync(
        string applicationCode,
        CreateRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentNullException.ThrowIfNull(request);

        return await PostAsync<CreateRoleRequest, ResponseBase<RoleResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/roles",
                   request,
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<RoleResponse>> UpdateAsync(
        string applicationCode,
        string code,
        UpdateRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<UpdateRoleRequest, ResponseBase<RoleResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/roles/{Uri.EscapeDataString(code)}",
                   request,
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<RoleResponse>> DeleteAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await DeleteAsync<ResponseBase<RoleResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/roles/{Uri.EscapeDataString(code)}",
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<RoleResponse>> GetByCodeAsync(
        string applicationCode,
        string code,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        return await GetAsync<ResponseBase<RoleResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/roles/{Uri.EscapeDataString(code)}",
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<RoleResponse>> AssignPermissionAsync(
        string applicationCode,
        string roleCode,
        AssignPermissionToRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(roleCode);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<AssignPermissionToRoleRequest, ResponseBase<RoleResponse>>(
                   $"{ApplicationsEndpoint}/{Uri.EscapeDataString(applicationCode)}/roles/{Uri.EscapeDataString(roleCode)}/permissions",
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
        => new("The API response could not be deserialized to ResponseBase<RoleResponse>.");
}
