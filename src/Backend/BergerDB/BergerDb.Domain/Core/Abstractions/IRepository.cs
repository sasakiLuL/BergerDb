namespace BergerDb.Domain.Core.Abstractions;

public interface IRepository<TEntity>
{
    void Add(TEntity entity);

    void Delete(TEntity entity);
}
