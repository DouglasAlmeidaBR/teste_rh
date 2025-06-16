using MediatR;

namespace Teste_RH.Application.Commands;

public record DeleteUserCommand(Guid userId) : IRequest;
