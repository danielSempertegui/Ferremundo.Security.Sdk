namespace Ferremundo.Security.Contracts.Common;

public sealed class ValidationErrorResponse
{
    public string Field { get; init; } = string.Empty;

    public IReadOnlyCollection<string> Errors { get; init; } = Array.Empty<string>();
}
