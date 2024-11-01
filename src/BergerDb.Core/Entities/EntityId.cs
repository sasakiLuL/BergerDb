namespace BergerDb.Core.Entities;

public interface IEntityId
{
    Guid Value { get; init; }
}
