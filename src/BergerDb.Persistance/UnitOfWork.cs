using BergerDb.Domain.Abstractions.Repositories;

namespace BergerDb.Persistance;

public class UnitOfWork(BergerDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
