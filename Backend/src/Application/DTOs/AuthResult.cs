namespace Application;

public record AuthResult
{
    public bool Success { get; init; }
    public string? Token { get; init; }
    public string? ErrorMessage { get; init; }

    // Статические фабричные методы
    public static AuthResult Successful(string token)
        => new AuthResult { Success = true, Token = token };

    public static AuthResult Failed(string errorMessage)
        => new AuthResult { Success = false, ErrorMessage = errorMessage };

    private AuthResult() { }
}