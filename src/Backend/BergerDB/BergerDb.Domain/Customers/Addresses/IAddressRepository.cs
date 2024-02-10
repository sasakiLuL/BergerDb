using BergerDb.Domain.Core.Abstractions;

namespace BergerDb.Domain.Customers.Addresses;

public interface IAddressRepository : IRepository<Address>
{
    Task<Address?> GetAddressByCustomerIdAsync(Guid customerID, CancellationToken cancellationToken = default);
}
