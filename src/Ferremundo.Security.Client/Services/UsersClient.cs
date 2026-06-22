using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Common;
using Ferremundo.Security.Contracts.Users.Requests;
using Ferremundo.Security.Contracts.Users.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class UsersClient : ExternalRestClientBase, IUsersClient
{
    private const string UsersEndpoint = "/api/v1/users";

    public UsersClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<UsersClient> logger)
        : base(
            httpClient,
            BuildExternalRestClientOptions(options.Value),
            authenticationStrategy,
            correlationProvider,
            logger)
    {
    }

    public async Task<ResponseBase<IReadOnlyCollection<SecurityUserResponse>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await GetAsync<ResponseBase<IReadOnlyCollection<SecurityUserResponse>>>(
                   UsersEndpoint,
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<IReadOnlyCollection<SecurityUserResponse>>.");
    }

    public async Task<ResponseBase<SecurityUserResponse>> RegisterAdUserAsync(
        RegisterAdUserRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await PostAsync<RegisterAdUserRequest, ResponseBase<SecurityUserResponse>>(
                   $"{UsersEndpoint}/ad",
                   request,
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<SecurityUserResponse>> UpdateAsync(
        string userName,
        UpdateSecurityUserRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<UpdateSecurityUserRequest, ResponseBase<SecurityUserResponse>>(
                   $"{UsersEndpoint}/{Uri.EscapeDataString(userName)}",
                   request,
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<SecurityUserResponse>> DeleteAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        return await DeleteAsync<ResponseBase<SecurityUserResponse>>(
                   $"{UsersEndpoint}/{Uri.EscapeDataString(userName)}",
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<SecurityUserResponse>> GetByUserNameAsync(
        string userName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        return await GetAsync<ResponseBase<SecurityUserResponse>>(
                   $"{UsersEndpoint}/{Uri.EscapeDataString(userName)}",
                   cancellationToken)
               ?? throw CreateEmptyResponseException();
    }

    public async Task<ResponseBase<SecurityUserResponse>> AssignRoleAsync(
        string userName,
        AssignUserRoleRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);
        ArgumentNullException.ThrowIfNull(request);

        return await PutAsync<AssignUserRoleRequest, ResponseBase<SecurityUserResponse>>(
                   $"{UsersEndpoint}/{Uri.EscapeDataString(userName)}/roles",
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
        => new("The API response could not be deserialized to ResponseBase<SecurityUserResponse>.");
}
