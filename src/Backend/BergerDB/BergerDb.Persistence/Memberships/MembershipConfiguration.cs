using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Customers.Memberships.Institutions;
namespace BergerDb.Persistence.Memberships;

public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.ToTable("memberships");

        builder.HasKey(membership => membership.Id);

        builder.Property(membership => membership.Id)
            .HasColumnName("membership_id");

        builder.HasOne(membership => membership.Customer)
            .WithOne(customer => customer.Membership)
            .HasForeignKey<Membership>(membership => membership.CustomerId)
            .IsRequired();

        builder.Property(membership => membership.IsDebtor)
            .HasColumnName("is_debtor")
            .IsRequired();

        builder.Property(membership => membership.IsRecivedDunning)
            .HasColumnName("is_recived_dunning")
            .IsRequired();

        builder.Property(membership => membership.IsRecivedInvoice)
            .HasColumnName("is_recived_invoice")
            .IsRequired();

        builder.Property(membership => membership.MemberType)
            .HasColumnName("member_type")
            .IsRequired();

        builder.OwnsOne(membership => membership.Institution, membershipBuilder =>
        {
            membershipBuilder.WithOwner();

            membershipBuilder.Property(membership => membership.Value)
                .HasColumnName("institution")
                .HasMaxLength(Institution.MaximumLength)
                .IsRequired();
        });

        builder.Property(membership => membership.EntryType)
            .HasColumnName("entry_type")
            .IsRequired();

        builder.Property(membership => membership.PaymentType)
            .HasColumnName("payment_type")
            .IsRequired();

        builder.Property(membership => membership.Amount)
            .HasColumnName("amount")
            .IsRequired();

        builder.OwnsOne(membership => membership.InvoiceSendedOn, membershipBuilder =>
        {
            membershipBuilder.WithOwner();

            membershipBuilder.Property(range => range.Current)
                .HasColumnName("current_invoice_sended_on");

            membershipBuilder.Property(range => range.Last)
                .HasColumnName("last_invoice_sended_on");
        });

        builder.OwnsOne(membership => membership.CreditReceivedOn, membershipBuilder =>
        {
            membershipBuilder.WithOwner();

            membershipBuilder.Property(range => range.Current)
                .HasColumnName("current_credit_received_on");

            membershipBuilder.Property(range => range.Last)
                .HasColumnName("last_credit_received_on");
        });

        builder.Property(membership => membership.TerminatedOn)
            .HasColumnName("terminated_on");

        builder.Property(membership => membership.DunningSendedOn)
            .HasColumnName("dunning_sended_on");

        builder.Property(membership => membership.CustomerId)
            .HasColumnName("customer_id");
    }
}
