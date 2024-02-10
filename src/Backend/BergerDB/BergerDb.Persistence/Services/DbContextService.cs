using BergerDb.Application.Core.Abstractions;

namespace BergerDb.Persistence.Services;

public class DbContextService : IDbContextService
{
    private readonly BergerDbContext _dbContext;

    public DbContextService(BergerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetQuaryable<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>().AsQueryable();
    }
}
