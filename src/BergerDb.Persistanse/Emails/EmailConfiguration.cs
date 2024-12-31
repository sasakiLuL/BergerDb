using BergerDb.Domain.Emails;
using BergerDb.Domain.PaymentProcesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistanse.Emails;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder
            .ToTable("Emails");

        builder
            .HasKey(email => email.Id);

        builder
            .HasIndex(email => email.Id)
            .IsUnique();

        builder
            .Property(cust => cust.Id)
            .HasConversion(
                custId => custId.Value,
                value => new EmailId(value));

        builder
            .Property(cust => cust.PaymentProcessId)
            .HasConversion(
                custId => custId.Value,
                value => new PaymentProcessId(value));

        builder
            .Property(email => email.EmailType)
            .IsRequired();

        builder
            .Property(email => email.SentOnUtc)
            .IsRequired();

        builder
            .Property(email => email.Subject)
            .IsRequired();

        builder
            .ComplexProperty(
                email => email.From, 
                builder =>
                {
                    builder
                        .Property(e => e.Value)
                        .IsRequired();
                });

        builder
            .ComplexProperty(
                email => email.To, 
                builder =>
                {
                    builder
                        .Property(e => e.Value)
                        .IsRequired();
                });

        builder
            .Property(email => email.BodyText)
            .IsRequired();

        builder
            .ComplexProperty(
                email => email.PdfMetadata, 
                builder =>
                {
                    builder
                        .Property(e => e.FileName)
                        .IsRequired();
                });
    }
}
