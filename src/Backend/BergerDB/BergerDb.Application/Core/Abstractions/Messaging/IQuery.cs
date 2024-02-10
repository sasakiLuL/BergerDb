using BergerDb.Domain.Core.Primitives.Result;
using MediatR;

namespace BergerDb.Application.Core.Abstractions.Messaging;

public interface IQuery<TResponce>
    : IRequest<Result<TResponce>>
{
}