using AutoMapper;
using Domain;
using MediatR;

namespace Application;

internal class GetCurrentRateQueryHandler
    : IRequestHandler<GetCurrentRateQuery, RateDto>
{
    private readonly IRateRepositoryGet _rateRepository;
    private readonly IMapper _mapper;

    public GetCurrentRateQueryHandler(
        IRateRepositoryGet rateRepository,
        IMapper mapper)
    {
        _rateRepository = rateRepository;
        _mapper = mapper;
    }

    public async Task<RateDto> Handle(
        GetCurrentRateQuery request,
        CancellationToken cancellationToken)
    {
        var rate = await _rateRepository.GetCurrentRateAsync(
            request.BaseCurrency,
            request.TargetCurrency);

        rate ??= new Rate
        {
            Value = 10.0m, // Дефолтное значение по ТЗ
            BaseCurrency = request.BaseCurrency,
            TargetCurrency = request.TargetCurrency
        };

        return _mapper.Map<RateDto>(rate);
    }
}
