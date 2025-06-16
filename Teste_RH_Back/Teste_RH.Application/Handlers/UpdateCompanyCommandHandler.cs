using MediatR;
using Teste_RH.Application.Commands;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Application.Handlers;

internal class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly IUserWriteRepository _userWrite;

    public UpdateCompanyCommandHandler(IUserWriteRepository userWrite)
    {
        _userWrite = userWrite;
    }

    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        await _userWrite.UpdateCompanyAsync(request.company);
    }
}