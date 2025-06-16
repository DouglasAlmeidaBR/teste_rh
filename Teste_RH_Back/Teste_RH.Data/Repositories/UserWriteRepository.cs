using Teste_RH.Core.Entities;
using Teste_RH.Core.Interfaces;
using Teste_RH.Data.Contexts;

namespace Teste_RH.Data.Repositories;

public class UserWriteRepository : IUserWriteRepository
{
    private readonly AppDbContext _context;

    public UserWriteRepository(AppDbContext context) => _context = context;

    public async Task<Guid> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCompanyAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        _context.Users.Remove(user!);
        await _context.SaveChangesAsync();
    }
}
