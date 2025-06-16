using Microsoft.EntityFrameworkCore;
using Teste_RH.Core.Entities;
using Teste_RH.Core.Interfaces;
using Teste_RH.Data.Contexts;

namespace Teste_RH.Data.Repositories;

public class UserReadRepository : IUserReadRepository
{
    private readonly AppDbContext _context;

    public UserReadRepository(AppDbContext context) => _context = context;

    public async Task<List<User>> GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetUserByIdAsync(Guid userId) => 
        await _context.Users.AsNoTracking()
                            .Include(_ => _.Company)
                                .ThenInclude(_ => _.Address)
                            .FirstOrDefaultAsync(_ => _.Id.Equals(userId));

    public async Task<bool> UserExistAsync(Guid userId) => await _context.Users.AsNoTracking().AnyAsync(_ => _.Id.Equals(userId));
}
