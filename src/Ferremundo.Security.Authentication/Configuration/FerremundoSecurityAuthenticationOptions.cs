namespace Ferremundo.Security.Authentication.Configuration;

public sealed class FerremundoSecurityAuthenticationOptions
{
    public const string SectionName = "FerremundoSecurity";

    public string Authority { get; init; } = string.Empty;

    public string Resource { get; init; } = string.Empty;

    public string IntrospectionClientId { get; init; } = string.Empty;

    public string IntrospectionClientSecret { get; init; } = string.Empty;

    public string PermissionClaimType { get; init; } = "permission";
}
