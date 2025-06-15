using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public record GetRecentPaymentsQuery(int Take = 5) 
    : IRequest<IEnumerable<PaymentDto>>;

