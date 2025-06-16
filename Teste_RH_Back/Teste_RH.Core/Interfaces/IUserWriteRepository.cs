using Teste_RH.Core.Entities;

namespace Teste_RH.Core.Interfaces;

public interface IUserWriteRepository
{
    Task<Guid> AddAsync(User user);
    Task UpdateAsync(User user);
    Task UpdateCompanyAsync(Company company);
    Task DeleteUserAsync(Guid userId);
}
