using AutoMapper;
using Domain;
using MediatR;

namespace Application;

internal class UpdateRateCommandHandler
    : IRequestHandler<UpdateRateCommand, RateDto>
{
    private readonly IRateRepositoryUpdate _rateWriteRepository;
    private readonly IRateRepositoryGet _rateReadRepository;
    private readonly IMapper _mapper;

    public UpdateRateCommandHandler(
        IRateRepositoryUpdate rateWriteRepository,
        IRateRepositoryGet rateReadRepository,
        IMapper mapper)
    {
        _rateWriteRepository = rateWriteRepository;
        _rateReadRepository = rateReadRepository;
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

        await _rateWriteRepository.UpdateRateAsync(rate);
        var updatedRate = await _rateReadRepository.GetCurrentRateAsync(
            request.BaseCurrency,
            request.TargetCurrency);

        return _mapper.Map<RateDto>(updatedRate);
    }
}
