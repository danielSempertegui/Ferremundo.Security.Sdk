namespace Ferremundo.Security.Client.Configuration;

public sealed class SecurityClientOptions
{
    public const string SectionName = "SecurityClient";

    public string BaseUrl { get; init; } = string.Empty;

    public int TimeoutSeconds { get; init; } = 30;

    public int RetryCount { get; init; } = 2;

    public int RetryDelayMilliseconds { get; init; } = 250;

    public string CorrelationHeaderName { get; init; } = "X-Correlation-Id";
}
