using BergerDb.Shared.Events;

namespace BergerDb.Shared.Entities;

public abstract class Entity
{
    public Entity(Guid id)
    {
        Id = id;
    }

    protected readonly List<IDomainEvent> _domainEvents = [];

    public Guid Id { get; private init; }

    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
}
