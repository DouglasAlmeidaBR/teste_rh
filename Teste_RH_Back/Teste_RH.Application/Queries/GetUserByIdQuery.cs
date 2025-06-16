using MediatR;
using Teste_RH.Core.Entities;

namespace Teste_RH.Application.Queries;

public record GetUserByIdQuery(Guid userId) : IRequest<User?>;
