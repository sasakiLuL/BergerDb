using BergerDb.Contracts.Common;

namespace BergerDb.Contracts.Users.Responses;

public record EmailConfigurationResponse(
    string Street,
    string City,
    string ZipCode,
    string PhoneNumber,
    string Email,
    string HomePage,
    string AccountName,
    string IBAN,
    string BIC,
    string GID,
    string TaxIdentificationNumber,
    string InvoicePdfBody,
    string InvoiceEmailSubject,
    string InvoiceEmailBody,
    string BillingRemindingEmailSubject,
    string BillingRemindingEmailBody,
    string DirectDebitingRemindingEmailSubject,
    string DirectDebitingRemindingEmailBody)
{
    public List<Link> Links { get; } = [];
}
