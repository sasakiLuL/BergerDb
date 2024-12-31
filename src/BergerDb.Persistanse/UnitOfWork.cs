using BergerDb.Application.Abstractions.Data;

namespace BergerDb.Persistanse;

public class UnitOfWork(BergerDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken token = default)
    {
        await context.SaveChangesAsync(token);
    }
}
