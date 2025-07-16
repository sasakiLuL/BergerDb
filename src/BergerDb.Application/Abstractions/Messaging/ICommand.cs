using BergerDb.Shared.Results;
using MediatR;

namespace BergerDb.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponce> : IRequest<Result<TResponce>>
{
}
