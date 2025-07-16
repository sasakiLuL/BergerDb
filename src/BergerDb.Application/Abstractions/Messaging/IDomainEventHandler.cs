using BergerDb.Shared.Events;
using MediatR;

namespace BergerDb.Application.Abstractions.Messaging;

internal interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> 
    where TEvent : IDomainEvent 
{
}
