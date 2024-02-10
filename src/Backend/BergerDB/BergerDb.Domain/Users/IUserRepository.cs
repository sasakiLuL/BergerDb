using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Users.UserNames;

namespace BergerDb.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetUsersAsync(CancellationToken token = default);

    Task<User?> GetUserByIdAsync(Guid id, CancellationToken token = default);

    Task<User?> GetUserByEmailAsync(Email email, CancellationToken token = default);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken token = default);

    Task<bool> IsUserNameUniqueAsync(UserName userName, CancellationToken tokens = default);
}
