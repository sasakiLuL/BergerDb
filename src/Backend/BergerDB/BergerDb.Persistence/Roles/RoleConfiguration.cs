using BergerDb.Domain.Users.RolePermissions;
using BergerDb.Domain.Users.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistence.Roles;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id)
            .HasColumnName("role_id");

        builder.Property(role => role.Name)
            .HasColumnName("role_name")
            .IsRequired();

        builder.HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasMany(role => role.Users)
            .WithMany(user => user.Roles)
            .UsingEntity(relation => {
                relation.ToTable("user_roles");
                relation.Property<Guid>("UsersId")
                    .HasColumnName("user_id");
                relation.Property<long>("RolesId")
                    .HasColumnName("role_id");
            });

        builder.HasData(Role.GetValues());
    }
}
