namespace BergerDb.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken token = default);
}
