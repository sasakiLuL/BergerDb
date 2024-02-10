namespace BergerDb.Application.Core.Abstractions;

public interface IDbContextService
{
    public IQueryable<TEntity> GetQuaryable<TEntity>() where TEntity : class;
}
