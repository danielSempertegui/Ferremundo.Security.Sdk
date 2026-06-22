namespace Ferremundo.Security.Contracts.Common;

public sealed class ResponseBase<T>
{
    public bool Success { get; init; }

    public string Code { get; init; } = default!;

    public string Message { get; init; } = default!;

    public T? Data { get; init; }
}
