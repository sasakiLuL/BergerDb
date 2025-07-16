using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Add(TEntity entity);

    void Delete(TEntity entity);

    Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
}
