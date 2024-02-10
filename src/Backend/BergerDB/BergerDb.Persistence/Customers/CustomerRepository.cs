using BergerDb.Domain.Customers;
using BergerDb.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistence.Customers;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(BergerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetCountAsync(CancellationToken token = default)
    {
        return await _dbContext.Customers.CountAsync(token);
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Customers.Include(c => c.Membership).Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.Id  == id, token);
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync(
        CancellationToken token = default)
    {
        return await _dbContext.Customers.Include(c => c.Membership).Include(c => c.Address)
            .ToListAsync(token);
    }
}
