using Ferremundo.Integrations.Rest;
using Ferremundo.Security.Client.Abstractions;
using Ferremundo.Security.Client.Authentication;
using Ferremundo.Security.Client.Configuration;
using Ferremundo.Security.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Ferremundo.Security.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurityClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExternalRestSupport();
        services.AddHttpContextAccessor();

        services.TryAddScoped<ISecurityAccessTokenProvider, CurrentRequestAccessTokenProvider>();
        services.TryAddScoped<ISecurityClientAuthenticationStrategy, SecurityClientAuthenticationStrategy>();

        services
            .AddOptions<SecurityClientOptions>()
            .Bind(configuration.GetSection(SecurityClientOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.BaseUrl), "SecurityClient:BaseUrl is required.")
            .ValidateOnStart();

        services.AddHttpClient<IApplicationsClient, ApplicationsClient>((serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SecurityClientOptions>>().Value;
            httpClient.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }
}
