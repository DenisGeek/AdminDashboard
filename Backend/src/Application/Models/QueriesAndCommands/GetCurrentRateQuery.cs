using Domain;
using MediatR;

namespace Application;

public record GetCurrentRateQuery(
    Currency BaseCurrency = Currency.USD,
    Currency TargetCurrency = Currency.Token)
    : IRequest<RateDto>;
