using BergerDb.Domain.Core.Abstractions;

namespace BergerDb.Domain.Users.Roles;

public interface IRoleRepository : IRepository<Role>
{
    Task<IEnumerable<Role>> GetRolesByIdAsync(long[] ids, CancellationToken token = default);

    Task<IEnumerable<Role>> GetRolesAsync(CancellationToken token = default);

    Task<Role?> GetRoleByIdAsync(long id, CancellationToken token = default);
}
