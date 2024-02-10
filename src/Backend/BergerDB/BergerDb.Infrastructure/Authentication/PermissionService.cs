using BergerDb.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BergerDb.Infrastructure.Authentication;

public class PermissionService : IPermissionService
{
    private readonly BergerDbContext _context;

    public PermissionService(BergerDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(Guid id)
    {
        var roles = await
           _context.Users
            .Include(u => u.Roles)
            .ThenInclude(p => p.Permissions)
            .Where(u => u.Id == id)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
