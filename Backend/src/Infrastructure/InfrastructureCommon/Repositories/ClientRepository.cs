using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommon;

internal class ClientRepository: IClientsRepositoryGetAll
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients
            .AsNoTracking()              // Для read-only операций
            .ToListAsync();
    }
}
