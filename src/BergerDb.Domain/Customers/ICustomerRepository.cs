using BergerDb.Domain.Abstractions;

namespace BergerDb.Domain.Customers;

public interface ICustomerRepository : IRepository<Customer, CustomerId>
{
    Task<IEnumerable<Customer>> GetQueryableAsync(Func<IQueryable<Customer>, IQueryable<Customer>>? filters = default, CancellationToken token = default);

    Task<int> GetCountAsync(CancellationToken token = default);
}
