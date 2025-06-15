using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureCommon;

public class UserRepository : IUserRepositoryGetByEmail
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}