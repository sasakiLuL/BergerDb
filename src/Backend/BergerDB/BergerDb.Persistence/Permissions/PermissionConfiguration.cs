using BergerDb.Domain.Users.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Domain.Permissions;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(perm => perm.Id);

        builder.Property(perm => perm.Id)
            .HasColumnName("permission_id");

        builder.Property(perm => perm.Name)
            .HasColumnName("permission_name");

        builder.ToTable("permissions");

        IEnumerable<Permission> permissions = Enum.GetValues<Users.Permissions.Enums.Permission>()
            .Select(p => new Permission(
                (long)p + 1, 
                p.ToString()
            ));

        builder.HasData(permissions);
    }
}
