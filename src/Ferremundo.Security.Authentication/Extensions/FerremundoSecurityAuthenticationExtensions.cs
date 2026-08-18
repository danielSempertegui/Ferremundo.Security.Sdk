using Ferremundo.Security.Authentication.Authorization;
using Ferremundo.Security.Authentication.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenIddict.Validation.AspNetCore;

namespace Ferremundo.Security.Authentication.Extensions;

public static class FerremundoSecurityAuthenticationExtensions
{
    public static IServiceCollection AddFerremundoSecurityAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var securityOptions = configuration
            .GetSection(FerremundoSecurityAuthenticationOptions.SectionName)
            .Get<FerremundoSecurityAuthenticationOptions>()
            ?? new FerremundoSecurityAuthenticationOptions();

        services
            .AddOptions<FerremundoSecurityAuthenticationOptions>()
            .Bind(configuration.GetSection(FerremundoSecurityAuthenticationOptions.SectionName))
            .Validate(options => !string.IsNullOrWhiteSpace(options.Authority), "FerremundoSecurity:Authority is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.Resource), "FerremundoSecurity:Resource is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.IntrospectionClientId), "FerremundoSecurity:IntrospectionClientId is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.IntrospectionClientSecret), "FerremundoSecurity:IntrospectionClientSecret is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.PermissionClaimType), "FerremundoSecurity:PermissionClaimType is required.")
            .ValidateOnStart();

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

        services
            .AddOpenIddict()
            .AddValidation(options =>
            {
                options.SetIssuer(new Uri(securityOptions.Authority));
                options.AddAudiences(securityOptions.Resource);
                options.SetClientId(securityOptions.IntrospectionClientId);
                options.SetClientSecret(securityOptions.IntrospectionClientSecret);
                options.UseIntrospection();
                options.UseSystemNetHttp();
                options.UseAspNetCore();
            });

        return services;
    }

    public static IServiceCollection AddFerremundoPermissionAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.Replace(ServiceDescriptor.Singleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IAuthorizationHandler, PermissionAuthorizationHandler>());
        services.Replace(ServiceDescriptor.Singleton<IAuthorizationMiddlewareResultHandler, ResponseAuthorizationMiddlewareResultHandler>());

        return services;
    }

    public static IApplicationBuilder UseFerremundoSecurityAuthentication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
