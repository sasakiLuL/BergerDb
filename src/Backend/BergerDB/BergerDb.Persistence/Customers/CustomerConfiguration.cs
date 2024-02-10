using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Customers.NameTitles;
using BergerDb.Domain.Customers.Notations;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistence.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(cust => cust.Id);

        builder.Property(cust => cust.Id)
            .HasColumnName("customer_id");

        builder.HasIndex(cust => cust.Id)
            .IsUnique();

        builder.HasOne(cust => cust.Address)
            .WithOne(address => address.Customer)
            .HasForeignKey<Address>(cust => cust.CustomerId)
            .IsRequired();

        builder.HasOne(cust => cust.Membership)
            .WithOne(member => member.Customer)
            .HasForeignKey<Membership>(cust => cust.CustomerId)
            .IsRequired();

        builder.OwnsOne(cust => cust.Prefix, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(e => e.Value)
            .HasMaxLength(Prefix.MaximumLength)
            .HasColumnName("prefix")
            .IsRequired();
        });

        builder.OwnsOne(cust => cust.FirstName, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(e => e.Value)
            .HasMaxLength(FirstName.MaximumLength)
            .HasColumnName("first_name")
            .IsRequired();
        });

        builder.OwnsOne(cust => cust.LastName, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(e => e.Value)
            .HasMaxLength(LastName.MaximumLength)
            .HasColumnName("last_name")
            .IsRequired();
        });

        builder.OwnsOne(cust => cust.Email, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(e => e.Value)
            .HasMaxLength(Email.MaximumLength)
            .HasColumnName("email")
            .IsRequired();
        });

        builder.OwnsOne(cust => cust.Notation, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(e => e.Value)
            .HasMaxLength(Notation.MaximumLength)
            .HasColumnName("notation")
            .IsRequired();
        });

        builder.Property(e => e.Sex)
            .HasColumnName("sex")
            .IsRequired();

        builder.Property(cust => cust.PersonalId)
            .HasColumnName("personal_id")
            .IsRequired();

        builder.Property(e => e.RegistrationDate)
            .HasColumnName("registration_date")
            .IsRequired();
    }
}
