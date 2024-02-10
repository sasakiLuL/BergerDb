namespace BergerDb.Domain.Core.Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; }

    DateTime? LastModifiedOnUtc { get; }
}
