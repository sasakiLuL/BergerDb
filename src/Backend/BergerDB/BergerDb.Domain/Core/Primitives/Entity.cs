namespace BergerDb.Domain.Core.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    private Entity()
    {
    }

    public Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected init; }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity other)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}
