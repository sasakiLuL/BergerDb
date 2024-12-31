using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Notations;
using BergerDb.Domain.Customers.Prefixes;
using BergerDb.Domain.Customers.ZipCodes;
using BergerDb.Domain.ValueObjects.Addresses;
using BergerDb.Domain.ValueObjects.EmailAddresses;
using BergerDb.Domain.ValueObjects.Names;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistanse.Customers;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable("Customers");        

        builder
            .HasKey(cust => cust.Id);

        builder
            .HasIndex(cust => cust.Id)
            .IsUnique();

        builder
            .Property(cust => cust.Id)
            .HasConversion(
                custId => custId.Value, 
                value => new CustomerId(value));

        builder
            .Property(e => e.PersonalId)
            .IsRequired();

        builder
            .ComplexProperty(
                cust => cust.Prefix, 
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(Prefix.MaximumLength)
                        .IsRequired();
                });

        builder
            .ComplexProperty(
                cust => cust.FirstName,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(Name.MaximumLength)
                        .IsRequired();
                });

        builder
            .ComplexProperty(
                cust => cust.LastName,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(Name.MaximumLength)
                        .IsRequired();
                });

        builder
            .Property(e => e.Sex)
            .IsRequired();

        builder
            .ComplexProperty(
                cust => cust.EmailAddress,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(EmailAddress.MaximumLength)
                        .IsRequired();
                });

        builder
            .Property(e => e.RegisteredOnUtc)
            .IsRequired();

        builder
            .Property(e => e.TerminatedOnUtc);

        builder
            .ComplexProperty(
                cust => cust.Notation,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(Notation.MaximumLength)
                        .IsRequired();
                });

        builder
            .ComplexProperty(
                cust => cust.Street,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(Address.MaximumLength)
                        .IsRequired();
                });

        builder
            .ComplexProperty(
                cust => cust.City,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(Address.MaximumLength)
                        .IsRequired();
                });

        builder
            .ComplexProperty(
                cust => cust.ZipCode,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(ZipCode.MaximumLength)
                        .IsRequired();
                });

        builder
            .Property(e => e.PaymentType)
            .IsRequired();

        builder
            .Property(e => e.MemberType)
            .IsRequired();

        builder
            .Property(e => e.EntryType)
            .IsRequired();

        builder
            .Property(e => e.SubscriptionCost)
            .IsRequired();

        builder
            .ComplexProperty(
                cust => cust.Institution,
                builder =>
                {
                    builder.Property(e => e.Value)
                        .HasMaxLength(Name.MaximumLength)
                        .IsRequired();
                });

        builder
            .HasMany(cust => cust.PaymentProcesses)
            .WithOne()
            .HasForeignKey(paymentProcess => paymentProcess.CustomerId);
    }
}
