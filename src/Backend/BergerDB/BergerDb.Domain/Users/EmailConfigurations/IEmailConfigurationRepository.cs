using BergerDb.Domain.Core.Abstractions;

namespace BergerDb.Domain.Users.EmailConfigurations;

public interface IEmailConfigurationRepository : IRepository<EmailConfiguration>
{
    Task<EmailConfiguration?> GetEmailConfigurationByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
