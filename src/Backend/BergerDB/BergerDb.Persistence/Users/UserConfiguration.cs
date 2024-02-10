using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.EmailConfigurations;
using BergerDb.Domain.Users.Passwords;
using BergerDb.Domain.Users.UserNames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistence.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.Id).IsUnique();

        builder.Property(user => user.Id)
            .HasColumnName("user_id");

        builder.HasOne(user => user.EmailConfiguration)
            .WithOne(emailConfiguration => emailConfiguration.User)
            .HasForeignKey<EmailConfiguration>(emailConfiguration => emailConfiguration.UserId);

        builder.OwnsOne(user => user.UserName, userNameBuilder =>
        {
            userNameBuilder.WithOwner();

            userNameBuilder.Property(userName => userName.Value)
                .HasColumnName("user_name")
                .HasMaxLength(UserName.MaximumLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.FirstName, firstNameBuilder =>
        {
            firstNameBuilder.WithOwner();

            firstNameBuilder.Property(p => p.Value)
                .HasColumnName("first_name")
                .HasMaxLength(FirstName.MaximumLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.LastName, lastNameBuilder =>
        {
            lastNameBuilder.WithOwner();

            lastNameBuilder.Property(p => p.Value)
                .HasColumnName("last_name")
                .HasMaxLength(LastName.MaximumLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(p => p.Value)
                .HasColumnName("email")
                .HasMaxLength(Email.MaximumLength)
                .IsRequired();
        });

        builder.Property<string>("_passwordHash")
                .HasField("_passwordHash")
                .HasColumnName("password_hash")
                .HasMaxLength(Password.HashLenght)
                .IsFixedLength(true)
                .IsRequired();

        builder.Property(user => user.CreatedOnUtc)
            .HasColumnName("created_on_utc")
        .IsRequired();

        builder.Property(user => user.LastModifiedOnUtc)
            .HasColumnName("last_modified_on_utc");

        builder.Property(user => user.DeletedOnUtc)
            .HasColumnName("deleted_on_utc");

        builder.Property(user => user.IsDeleted)
            .HasColumnName("deleted")
            .HasDefaultValue(false);

        builder.HasQueryFilter(user => !user.IsDeleted);
    }
}
