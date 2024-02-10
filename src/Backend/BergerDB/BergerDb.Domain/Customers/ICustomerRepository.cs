using BergerDb.Domain.Core.Abstractions;

namespace BergerDb.Domain.Customers;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken token = default);

    Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken token = default);

    Task<int> GetCountAsync(CancellationToken token = default);
}
