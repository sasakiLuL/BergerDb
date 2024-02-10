using BergerDb.Domain.Core.Primitives.Result;
using MediatR;

namespace BergerDb.Application.Core.Abstractions.Messaging;

public interface ICommandBase
{
}

public interface ICommand
    : IRequest<Result>, ICommandBase
{
}

public interface ICommand<TResponce>
    : IRequest<Result<TResponce>>, ICommandBase
{
}