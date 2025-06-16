using MediatR;
using Teste_RH.Application.Commands;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserWriteRepository _userWrite;

    public CreateUserCommandHandler(IUserWriteRepository userWrite)
    {
        _userWrite = userWrite;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userWrite.AddAsync(request.User);
    }
}
