using BergerDb.Domain.PaymentProcesses;
using BergerDb.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistanse.Payments;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder
            .ToTable("Payments");

        builder
            .HasKey(payment => payment.Id);

        builder
            .HasIndex(payment => payment.Id)
            .IsUnique();

        builder
            .Property(payment => payment.Value)
            .IsRequired();

        builder
            .Property(payment => payment.PaidOnUtc)
            .IsRequired();

        builder
            .HasOne<PaymentProcess>()
            .WithOne()
            .HasForeignKey<Payment>(paymentProcess => paymentProcess.PaymentProcessId);
    }
}
