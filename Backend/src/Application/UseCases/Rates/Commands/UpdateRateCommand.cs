using Domain;
using MediatR;

namespace Application;
public record UpdateRateCommand(
    decimal NewRate,
    Currency BaseCurrency = Currency.USD,
    Currency TargetCurrency = Currency.Token)
    : IRequest<RateDto>;
