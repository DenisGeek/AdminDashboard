using AutoMapper;
using MediatR;

namespace Application;

internal class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, List<ClientDto>>
{
    private readonly IClientsRepositoryGetAll _repository;
    private readonly IMapper _mapper;

    public GetClientsQueryHandler(IClientsRepositoryGetAll repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _repository.GetAllAsync();
        return _mapper.Map<List<ClientDto>>(clients);
    }
}
