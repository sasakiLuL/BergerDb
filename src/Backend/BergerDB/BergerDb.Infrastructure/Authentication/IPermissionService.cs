namespace BergerDb.Infrastructure.Authentication;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid id);
}
