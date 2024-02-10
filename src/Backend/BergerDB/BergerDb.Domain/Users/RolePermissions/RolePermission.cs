namespace BergerDb.Domain.Users.RolePermissions;

public class RolePermission
{
    public RolePermission(long roleId, long permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public long RoleId { get; }

    public long PermissionId { get; }
}
