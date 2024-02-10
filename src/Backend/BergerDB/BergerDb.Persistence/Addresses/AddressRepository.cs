using BergerDb.Domain.Customers.Addresses;
using BergerDb.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistence.Addresses;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(BergerDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Address?> GetAddressByCustomerIdAsync(Guid customerID, CancellationToken token)
    {
        return _dbContext.Addresses.FirstOrDefaultAsync(a => a.CustomerId == customerID);
    }
}
