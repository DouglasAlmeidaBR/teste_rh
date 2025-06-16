using MediatR;
using Teste_RH.Application.Commands;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Application.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserWriteRepository _userWrite;

    public UpdateUserCommandHandler(IUserWriteRepository userWrite)
    {
        _userWrite = userWrite;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _userWrite.UpdateAsync(request.User);
    }
}
