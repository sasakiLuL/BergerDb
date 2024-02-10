using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.UserNames;
using BergerDb.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistence.Users;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(BergerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken token)
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken token)
    {
        return await _dbContext.Users.AllAsync(u => u.Email.Value != email.Value);
    }

    public async Task<User?> GetUserByEmailAsync(Email email, CancellationToken token)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Value == email.Value);
    }

    public async Task<bool> IsUserNameUniqueAsync(UserName userName, CancellationToken token)
    {
        return await _dbContext.Users.AllAsync(u => u.UserName.Value != userName.Value);
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken token)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
