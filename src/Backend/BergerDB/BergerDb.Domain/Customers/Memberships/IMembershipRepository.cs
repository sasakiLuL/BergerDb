using BergerDb.Domain.Core.Abstractions;

namespace BergerDb.Domain.Customers.Memberships;

public interface IMembershipRepository : IRepository<Membership>
{
    Task<Membership?> GetMembershipByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
}
