using Domain;
using MediatR;

namespace Application;

public record GetAllRatesQuery : IRequest<IEnumerable<Rate>>;