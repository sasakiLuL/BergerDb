using BergerDb.Domain.Core.Abstractions;

namespace BergerDb.Persistence.Abstractions;

public abstract class Repository<TEntity>
    : IRepository<TEntity> where TEntity : class
{
    protected readonly BergerDbContext _dbContext;

    public Repository(BergerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TEntity entity)
    {
        _dbContext.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Remove(entity);
    }
}
