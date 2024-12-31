using BergerDb.Shared.Events;

namespace BergerDb.Shared.Entities;

public abstract class Entity<TId> where TId : EntityId
{
    public Entity(TId id)
    {
        Id = id;
    }

    protected readonly List<IDomainEvent> _domainEvents = [];

    public TId Id { get; private init; }

    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
}
