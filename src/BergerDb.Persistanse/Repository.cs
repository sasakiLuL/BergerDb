using BergerDb.Domain.Abstractions;
using BergerDb.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistanse;

public abstract class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : EntityId
{
    protected readonly BergerDbContext _context;

    public Repository(BergerDbContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default)
    {
        return await _context.Set<TEntity>().ToListAsync(token);
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken token = default)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(token);
    }
}
