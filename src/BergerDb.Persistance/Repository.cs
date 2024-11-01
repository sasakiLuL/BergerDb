using BergerDb.Core.Entities;
using BergerDb.Domain.Abstractions.Repositories;

namespace BergerDb.Persistance;

public class Repository<TValue, TModel>
    : IRepository<TValue, TModel> where TValue
    : Entity<TModel> where TModel : IModel
{
    protected readonly BergerDbContext _context;

    public Repository(BergerDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TValue entity, CancellationToken token = default)
    {
        await _context.AddAsync(entity.Model, token);
    }

    public void Delete(TValue entity)
    {
        _context.Remove(entity.Model);
    }
}
