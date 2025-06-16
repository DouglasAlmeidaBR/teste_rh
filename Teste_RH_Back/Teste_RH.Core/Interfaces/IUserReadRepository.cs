using Teste_RH.Core.Entities;

namespace Teste_RH.Core.Interfaces;

public interface IUserReadRepository
{
    Task<List<User>> GetAllAsync();
    Task<bool> UserExistAsync(Guid userId);
    Task<User?> GetUserByIdAsync(Guid userId);
}
