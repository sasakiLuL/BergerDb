using BergerDb.Domain.Users.Permissions.Enums;
using BergerDb.Domain.Users.RolePermissions;
using BergerDb.Domain.Users.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistence.RolePermisions;

public class RolePermissinConfiguretion : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.Property(x => x.PermissionId)
            .HasColumnName("permission_id");

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id");

        builder.HasData(
            Create(Role.Admin, Permission.UsersCreate),
            Create(Role.Admin, Permission.UsersRead),
            Create(Role.Admin, Permission.UsersUpdate),
            Create(Role.Admin, Permission.UsersDelete),
            Create(Role.Admin, Permission.CustomersCreate),
            Create(Role.Admin, Permission.CustomersRead),
            Create(Role.Admin, Permission.CustomersUpdate),
            Create(Role.Admin, Permission.CustomersDelete),
            Create(Role.Visitor, Permission.UsersUpdate),
            Create(Role.Visitor, Permission.UsersDelete),
            Create(Role.Visitor, Permission.CustomersRead));
    }

    private static RolePermission Create(
        Role role, 
        Permission permission)
    {
        return new RolePermission(
            role.Id, 
            (long)permission + 1
        );
    }
}
