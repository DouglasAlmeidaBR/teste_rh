using MediatR;
using Teste_RH.Application.Commands;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Application.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserWriteRepository _userWrite;

    public DeleteUserCommandHandler(IUserWriteRepository userWrite)
    {
        _userWrite = userWrite;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.userId == Guid.Empty)
            throw new ArgumentException("");

        await _userWrite.DeleteUserAsync(request.userId);
    }
}
