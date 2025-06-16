using MediatR;
using Teste_RH.Application.Queries;
using Teste_RH.Core.Entities;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Application.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserReadRepository _userRead;

    public GetUserByIdQueryHandler(IUserReadRepository userRead)
    {
        _userRead = userRead;
    }

    public async Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userRead.GetUserByIdAsync(request.userId);
    }
}
