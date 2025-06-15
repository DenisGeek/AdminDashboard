using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public class RateProfile : Profile
{
    public RateProfile()
    {
        CreateMap<Rate, RateDto>()
            .ForMember(dest => dest.BaseCurrency,
                opt => opt.MapFrom(src => src.BaseCurrency.ToString()))
            .ForMember(dest => dest.TargetCurrency,
                opt => opt.MapFrom(src => src.TargetCurrency.ToString()));
    }
}