using BergerDb.Domain.Users.EmailConfigurations;
using BergerDb.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistence.EmailConfigurations;

public class EmailConfigurationRepository : Repository<EmailConfiguration>, IEmailConfigurationRepository
{
    public EmailConfigurationRepository(BergerDbContext dbContext) : base(dbContext) { }

    public Task<EmailConfiguration?> GetEmailConfigurationByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _dbContext.EmailConfigurations.FirstOrDefaultAsync(c => c.UserId == userId);
    }
}
