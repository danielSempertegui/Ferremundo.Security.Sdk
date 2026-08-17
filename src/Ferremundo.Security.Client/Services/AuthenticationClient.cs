using Ferremundo.Integrations.Rest;
using Ferremundo.Integrations.Rest.Abstractions.Correlation;
using Ferremundo.Integrations.Rest.Configuration;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Contracts.Authentication.Requests;
using Ferremundo.Security.Contracts.Authentication.Responses;
using Ferremundo.Security.Contracts.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client.Services;

public sealed class AuthenticationClient : ExternalRestClientBase, IAuthenticationClient
{
    private const string AuthEndpoint = "/api/v1/auth";

    public AuthenticationClient(
        HttpClient httpClient,
        IOptions<SecurityClientOptions> options,
        ISecurityClientAuthenticationStrategy authenticationStrategy,
        IExternalCorrelationProvider correlationProvider,
        ILogger<AuthenticationClient> logger)
        : base(
            httpClient,
            BuildExternalRestClientOptions(options.Value),
            authenticationStrategy,
            correlationProvider,
            logger)
    {
    }

    public async Task<ResponseBase<LoginResponse>> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await PostAsync<LoginRequest, ResponseBase<LoginResponse>>(
                   $"{AuthEndpoint}/login",
                   request,
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<LoginResponse>.");
    }

    public async Task<ResponseBase<LogoutResponse>> LogoutAsync(
        CancellationToken cancellationToken = default)
    {
        return await PostAsync<object, ResponseBase<LogoutResponse>>(
                   $"{AuthEndpoint}/logout",
                   new { },
                   cancellationToken)
               ?? throw new InvalidOperationException("The API response could not be deserialized to ResponseBase<LogoutResponse>.");
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
