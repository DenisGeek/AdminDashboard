using AutoMapper;
using Domain;

namespace Application;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, PaymentDto>();
    }
}
