using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Users.UpdateEmailConfiguration;

public record UpdateEmailConfigurationCommand(
    Guid Id,
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
    string DirectDebitingRemindingEmailBody) : ICommand;
