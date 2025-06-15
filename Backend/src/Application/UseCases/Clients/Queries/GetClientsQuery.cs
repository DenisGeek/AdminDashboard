using MediatR;

namespace Application;

public record GetClientsQuery : IRequest<List<ClientDto>>;