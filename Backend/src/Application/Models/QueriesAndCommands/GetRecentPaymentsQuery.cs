using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public class GetRecentPaymentsQuery(int Take = 5) : IRequest<IEnumerable<PaymentDto>>
{
    public int Take { get; } = Take;
}
