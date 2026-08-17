namespace Ferremundo.Security.Contracts.Authentication.Responses;

public sealed class LoginResponse
{
    public string UserName { get; init; } = string.Empty;

    public string DisplayName { get; init; } = string.Empty;

    public string? Email { get; init; }

    public string? ReturnUrl { get; init; }
}
