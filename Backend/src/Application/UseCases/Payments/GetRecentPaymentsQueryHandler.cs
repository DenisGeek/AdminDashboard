using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public class GetRecentPaymentsQueryHandler
    : IRequestHandler<GetRecentPaymentsQuery, IEnumerable<PaymentDto>>
{
    private readonly IPaymentRepositoryGetRecent _paymentRepository;
    private readonly IMapper _mapper;

    public GetRecentPaymentsQueryHandler(
        IPaymentRepositoryGetRecent paymentRepository,
        IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentDto>> Handle(
        GetRecentPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetRecentPaymentsAsync(request.Take);
        return _mapper.Map<IEnumerable<PaymentDto>>(payments);
    }
}