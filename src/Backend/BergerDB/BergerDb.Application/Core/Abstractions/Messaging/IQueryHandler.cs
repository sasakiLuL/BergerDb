using BergerDb.Domain.Core.Primitives.Result;
using MediatR;

namespace BergerDb.Application.Core.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
