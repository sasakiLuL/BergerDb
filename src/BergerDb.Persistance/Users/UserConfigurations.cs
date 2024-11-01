using BergerDb.Domain.Users;
using BergerDb.Domain.Users.Emails;
using BergerDb.Domain.Users.UserIds;
using BergerDb.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistance.Users;

public class UserConfigurations : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.HasIndex(t => t.Id);

        builder.ComplexProperty(p => p.Email, propBuilder =>
        {
            propBuilder.Property(x => x.Value)
                .HasColumnName("email")
                .HasMaxLength(Email.MaximumLength)
                .IsRequired();
        });

        builder.Property(p => p.PasswordHash)
            .IsRequired();

        builder.ComplexProperty(p => p.ProfileId, propBuilder =>
        {
            propBuilder.Property(x => x.Value)
                .HasColumnName("profile_id")
                .IsRequired();
        });
    }
}
