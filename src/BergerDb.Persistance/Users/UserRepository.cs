using BergerDb.Domain.Users;
using BergerDb.Domain.Users.Emails;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistance.Users;

public class UserRepository(BergerDbContext dbContext)
    : Repository<User, UserModel>(dbContext), IUserRepository
{
    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user is null ? null : new User(user);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        return !await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
}
