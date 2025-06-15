namespace Application;
public record PaymentDto(
    Guid Id,
    Guid ClientId,
    decimal Amount,
    DateTime Date,
    string Description,
    string Status);