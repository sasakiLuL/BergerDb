using BergerDb.Shared.Results;
using MediatR;

namespace BergerDb.Application.Abstractions.Messaging;

public interface IQuery<TResponce> : IRequest<Result<TResponce>>
{
}
