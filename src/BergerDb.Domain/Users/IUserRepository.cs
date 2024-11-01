using BergerDb.Domain.Abstractions.Repositories;
using BergerDb.Domain.Users.Emails;

namespace BergerDb.Domain.Users;

public interface IUserRepository : IRepository<User, UserModel>
{
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
}
