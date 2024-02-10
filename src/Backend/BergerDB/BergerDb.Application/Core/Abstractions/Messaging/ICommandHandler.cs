using BergerDb.Domain.Core.Primitives.Result;
using MediatR;

namespace BergerDb.Application.Core.Abstractions.Messaging;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
