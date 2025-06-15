namespace Application;

public record ClientDto(
    Guid Id,
    string Name,
    string Email,
    decimal BalanceInTokens,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
