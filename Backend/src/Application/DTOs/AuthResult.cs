namespace Application;

public record AuthResult
{
    public bool Success { get; init; }
    public AuthTokenResponseDto? Token { get; init; }
    public string? ErrorMessage { get; init; }

    // Статические фабричные методы
    public static AuthResult Successful(AuthTokenResponseDto tokens)
        => new AuthResult { Success = true, Token = tokens };

    public static AuthResult Failed(string errorMessage)
        => new AuthResult { Success = false, ErrorMessage = errorMessage };

    private AuthResult() { }
}