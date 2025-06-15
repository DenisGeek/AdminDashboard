using MediatR;

namespace Application;

public class GetClientsQuery : IRequest<List<ClientDto>>
{
}
