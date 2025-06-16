using Domain;

namespace Application;
public record PaymentDto(
    Guid Id,
    Guid ClientId,
    decimal Amount,
    Currency Currency,
    DateTime Date,
    string Description,
    string Status);