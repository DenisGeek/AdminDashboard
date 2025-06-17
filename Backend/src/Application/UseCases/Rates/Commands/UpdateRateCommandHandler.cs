using AutoMapper;
using Domain;
using MediatR;

namespace Application;

internal class UpdateRateCommandHandler
    : IRequestHandler<UpdateRateCommand, RateDto>
{
    private readonly IRateRepository _rateRepository;
    private readonly IMapper _mapper;

    public UpdateRateCommandHandler(
        IRateRepository rateRepository,
        IMapper mapper)
    {
        _rateRepository = rateRepository;
        _mapper = mapper;
    }

    public async Task<RateDto> Handle(
        UpdateRateCommand request,
        CancellationToken cancellationToken)
    {
        var rate = new Rate
        {
            Value = request.NewRate,
            BaseCurrency = request.BaseCurrency,
            TargetCurrency = request.TargetCurrency
        };

        await _rateRepository.UpdateRateAsync(rate);
        var updatedRate = await _rateRepository.GetCurrentRateAsync(
            request.BaseCurrency,
            request.TargetCurrency);

        return _mapper.Map<RateDto>(updatedRate);
    }
}
