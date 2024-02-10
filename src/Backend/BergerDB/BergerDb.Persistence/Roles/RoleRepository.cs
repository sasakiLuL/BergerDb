using BergerDb.Domain.Users.Roles;
using BergerDb.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Persistence.Roles;

public sealed class RoleRepository : Repository<Role>, IRoleRepository
{

    public RoleRepository(BergerDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Role>> GetRolesByIdAsync(long[] id, CancellationToken token)
    {
        return await _dbContext.Roles.Where(r => id.Contains(r.Id)).ToListAsync(token);
    }

    public async Task<Role?> GetRoleByIdAsync(long id, CancellationToken token)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<IEnumerable<Role>> GetRolesAsync(CancellationToken token)
    {
        return await _dbContext.Roles.ToListAsync(token);
    }
}
