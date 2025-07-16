using BergerDb.Domain.Customers;
using BergerDb.Domain.PaymentProcesses;
using BergerDb.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistanse.PaymentProcesses;

public class PaymentProcessConfiguration : IEntityTypeConfiguration<PaymentProcess>
{
    public void Configure(EntityTypeBuilder<PaymentProcess> builder)
    {
        builder
            .ToTable("PaymentProcesses");

        builder
            .HasKey(paymentProcess => paymentProcess.Id);

        builder
            .HasIndex(paymentProcess => paymentProcess.Id)
            .IsUnique();

        builder
            .HasDiscriminator(paymentProcess => paymentProcess.PaymentType)
            .HasValue<Billing>(PaymentType.Billing)
            .HasValue<Debiting>(PaymentType.Debiting);

        builder
            .Property(paymentProcess => paymentProcess.PaymentType)
            .IsRequired();

        builder
            .Property(paymentProcess => paymentProcess.PaymentId);

        builder
            .Property(paymentProcess => paymentProcess.CustomerId);

        builder
            .HasMany(paymentProcess => paymentProcess.Emails)
            .WithOne()
            .HasForeignKey(email => email.PaymentProcessId);

        builder
            .HasOne<Payment>()
            .WithOne()
            .HasForeignKey<PaymentProcess>(paymentProcess => paymentProcess.PaymentId);
    }
}
