using MediatR;
using Teste_RH.Application.Commands;
using Teste_RH.Application.Queries;
using Teste_RH.Core.Entities;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Application.Services;

public class UserService : IUserService
{
    private readonly IMediator _mediator;
    
    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Guid> AddAsync(User user)
    {
        var command = new CreateUserCommand(user);

        return await _mediator.Send(command);
    }

    public async Task UpdateAsync(User user)
    {
        var command = new UpdateUserCommand(user);
        await _mediator.Send(command);
    }

    public async Task UpdateCompanyAsync(Company company)
    {
        var command = new UpdateCompanyCommand(company);
        await _mediator.Send(command);
    }

    public async Task<List<User>> GetAllAsync() => await _mediator.Send(new GetAllUsersQuery());

    public async Task<User?> GetUserByIdAsync(Guid userId) => await _mediator.Send(new GetUserByIdQuery(userId));

    public async Task DeleteUserAsync(Guid userId) => await _mediator.Send(new DeleteUserCommand(userId));
}
