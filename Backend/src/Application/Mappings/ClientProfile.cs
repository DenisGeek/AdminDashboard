using AutoMapper;
using Domain;

namespace Application;

internal class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ClientDto>();
    }
}
