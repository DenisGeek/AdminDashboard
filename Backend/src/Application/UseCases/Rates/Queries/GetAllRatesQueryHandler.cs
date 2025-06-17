using Domain;
using MediatR;

namespace Application;

public class GetAllRatesQueryHandler : IRequestHandler<GetAllRatesQuery, IEnumerable<Rate>>
{
    private readonly IRateRepository _rateRepository;

    public GetAllRatesQueryHandler(IRateRepository rateRepository)
    {
        _rateRepository = rateRepository;
    }

    public async Task<IEnumerable<Rate>> Handle(GetAllRatesQuery request, CancellationToken cancellationToken)
    {
        return await _rateRepository.GetRateAllAsync();
    }
}
