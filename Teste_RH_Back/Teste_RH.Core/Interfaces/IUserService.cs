using Teste_RH.Core.Entities;

namespace Teste_RH.Core.Interfaces;
public interface IUserService
{
    Task<Guid> AddAsync(User user);
    Task<List<User>> GetAllAsync();
    Task UpdateAsync(User user);
    Task UpdateCompanyAsync(Company company);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);
}
