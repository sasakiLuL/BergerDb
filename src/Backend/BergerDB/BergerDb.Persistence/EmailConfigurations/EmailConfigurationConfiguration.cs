using BergerDb.Domain.Users.EmailConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BergerDb.Persistence.EmailConfigurations;

public class EmailConfigurationConfiguration : IEntityTypeConfiguration<EmailConfiguration>
{
    public void Configure(EntityTypeBuilder<EmailConfiguration> builder)
    {
        builder.ToTable("email_configuration");

        builder.HasKey(conf => conf.Id);

        builder.Property(conf => conf.Id)
            .HasColumnName("email_configuration_id");

        builder.HasOne(conf => conf.User)
            .WithOne(user => user.EmailConfiguration)
            .HasForeignKey<EmailConfiguration>(conf => conf.UserId)
            .IsRequired();

        builder.Property(conf => conf.Street)
            .HasColumnName("street")
            .IsRequired();

        builder.Property(conf => conf.City)
            .HasColumnName("city")
            .IsRequired();

        builder.Property(conf => conf.ZipCode)
            .HasColumnName("postalcode")
            .IsRequired();

        builder.Property(conf => conf.PhoneNumber)
            .HasColumnName("phone_number")
            .IsRequired();

        builder.Property(conf => conf.PhoneNumber)
            .HasColumnName("phone_number")
            .IsRequired();

        builder.Property(conf => conf.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Property(conf => conf.HomePage)
            .HasColumnName("home_page")
            .IsRequired();

        builder.Property(conf => conf.IBAN)
            .HasColumnName("iban")
            .IsRequired();

        builder.Property(conf => conf.BIC)
            .HasColumnName("bic")
            .IsRequired();

        builder.Property(conf => conf.GID)
            .HasColumnName("gid")
            .IsRequired();

        builder.Property(conf => conf.TaxIdentificationNumber)
            .HasColumnName("tax_identification_number")
            .IsRequired();

        builder.Property(conf => conf.InvoicePdfBody)
            .HasColumnName("invoice_pdf_body")
            .IsRequired();

        builder.Property(conf => conf.InvoiceEmailSubject)
            .HasColumnName("invoice_email_subject")
            .IsRequired();

        builder.Property(conf => conf.InvoiceEmailBody)
            .HasColumnName("invoice_email_body")
            .IsRequired();

        builder.Property(conf => conf.BillingRemindingEmailSubject)
            .HasColumnName("billing_reminding_email_subject")
            .IsRequired();

        builder.Property(conf => conf.BillingRemindingEmailBody)
            .HasColumnName("billing_reminding_email_body")
            .IsRequired();

        builder.Property(conf => conf.DirectDebitingRemindingEmailSubject)
            .HasColumnName("direct_debiting_reminding_email_subject")
            .IsRequired();

        builder.Property(conf => conf.DirectDebitingRemindingEmailBody)
            .HasColumnName("direct_debiting_reminding_email_body")
            .IsRequired();

        builder.Property(conf => conf.AccountName)
            .HasColumnName("account_name")
            .IsRequired();

        builder.Property(conf => conf.UserId)
            .HasColumnName("user_id")
            .IsRequired();
    }
}
