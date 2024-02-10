using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Addresses.AddressNames;
using BergerDb.Domain.Customers.Addresses.PostalCodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistence.Addresses;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");

        builder.HasKey(address => address.Id);

        builder.Property(address => address.Id)
            .HasColumnName("address_id");
            
        builder.HasOne(address => address.Customer)
            .WithOne(customer => customer.Address)
            .HasForeignKey<Address>(address => address.CustomerId)
            .IsRequired();

        builder.OwnsOne(address => address.Street, streetBuilder =>
        {
            streetBuilder.WithOwner();

            streetBuilder.Property(street => street.Value)
            .HasMaxLength(AddressName.MaximumLength)
            .HasColumnName("street")
            .IsRequired();
        });

        builder.OwnsOne(address => address.City, cityBuilder =>
        {
            cityBuilder.WithOwner();

            cityBuilder.Property(city => city.Value)
            .HasMaxLength(AddressName.MaximumLength)
            .HasColumnName("city")
            .IsRequired();
        });

        builder.OwnsOne(address => address.ZipCode, codeBuilder =>
        {
            codeBuilder.WithOwner();

            codeBuilder.Property(code => code.Value)
            .HasMaxLength(ZipCode.MaximumLenght)
            .HasColumnName("zip_code")
            .IsRequired();
        });

        builder.Property(address => address.CustomerId)
            .HasColumnName("customer_id");
    }
}
