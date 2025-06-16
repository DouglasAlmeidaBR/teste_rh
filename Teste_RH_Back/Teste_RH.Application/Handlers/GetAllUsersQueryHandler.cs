using MediatR;
using Teste_RH.Application.Queries;
using Teste_RH.Core.Entities;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Application.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly IUserReadRepository _userRead;

    public GetAllUsersQueryHandler(IUserReadRepository userRead)
    {
        _userRead = userRead;
    }

    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userRead.GetAllAsync();
    }
}
