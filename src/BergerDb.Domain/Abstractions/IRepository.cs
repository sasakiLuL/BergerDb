using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Abstractions;

public interface IRepository<TEntity, TEntityId> where TEntity : Entity<TEntityId> where TEntityId : EntityId
{
    void Add(TEntity entity);

    void Delete(TEntity entity);

    Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default);

    Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken token = default);
}
