using BergerDb.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistanse.Customers;

public class CustomerRepository : Repository<Customer, CustomerId>, ICustomerRepository
{
    public CustomerRepository(BergerDbContext context) : base(context)
    {
    }

    public async Task<int> GetCountAsync(CancellationToken token = default)
    {
        return await _context.Customers.CountAsync(token);
    }

    public async Task<IEnumerable<Customer>> GetQueryableAsync(Func<IQueryable<Customer>, IQueryable<Customer>>? filters = null, CancellationToken token = default)
    {
        var query = _context.Customers
            .Include(c => c.PaymentProcesses)
            .ThenInclude(p => p.Emails)
            .AsQueryable();

        if (filters is not null)
        {
            query = filters(_context.Customers);
        }

        return await query.ToListAsync(token);
    }
}
