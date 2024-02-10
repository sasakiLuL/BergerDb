using BergerDb.Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BergerDb.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BergerDbContext _dbContext;

    public UnitOfWork(BergerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default)
        => _dbContext.Database.BeginTransactionAsync(token);

    public Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        UpdateAuditableEntities();

        UpdateSoftDeletableEntities();

        return _dbContext.SaveChangesAsync(token);
    }

    private void UpdateAuditableEntities()
    {
        foreach (EntityEntry<IAuditableEntity> entityEntry in _dbContext.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = DateTime.UtcNow;
            }
            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.LastModifiedOnUtc)).CurrentValue = DateTime.UtcNow;
            }
        }
    }

    private void UpdateSoftDeletableEntities()
    {
        foreach (EntityEntry<ISoftDeletableEntity> entityEntry in _dbContext.ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State != EntityState.Deleted)
            {
                continue;
            }

            entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = DateTime.UtcNow;

            entityEntry.Property(nameof(ISoftDeletableEntity.IsDeleted)).CurrentValue = true;

            entityEntry.State = EntityState.Modified;

            UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
        {
            return;
        }

        foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry?.State == EntityState.Deleted))
        {
            referenceEntry.TargetEntry!.State = EntityState.Unchanged;

            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }
}
