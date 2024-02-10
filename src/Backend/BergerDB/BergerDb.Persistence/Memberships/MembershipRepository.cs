using BergerDb.Domain.Customers.Memberships;
using BergerDb.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistence.Memberships;

public class MembershipRepository : Repository<Membership>, IMembershipRepository
{
    public MembershipRepository(BergerDbContext dbContext) : base(dbContext) {}

    public Task<Membership?> GetMembershipByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Memberships.FirstOrDefaultAsync(a => a.CustomerId == customerId);
    }
}
