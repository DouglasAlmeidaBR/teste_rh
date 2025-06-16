using MediatR;
using Teste_RH.Core.Entities;

namespace Teste_RH.Application.Commands;

public record CreateUserCommand(User User) : IRequest<Guid>;
