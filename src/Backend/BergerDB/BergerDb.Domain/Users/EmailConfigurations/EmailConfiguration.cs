using BergerDb.Domain.Core.Primitives;

namespace BergerDb.Domain.Users.EmailConfigurations;

public class EmailConfiguration : Entity
{
    private EmailConfiguration() : base(Guid.NewGuid()) {}

    public EmailConfiguration(
        string street,
        string city,
        string zipCode,
        string phoneNumber,
        string email,
        string homePage,
        string accountName,
        string iBAN,
        string bIC,
        string gID,
        string taxIdentificationNumber,
        string invoicePdfBody,
        string invoiceEmailSubject,
        string invoiceEmailBody,
        string billingRemindingEmailSubject,
        string billingRemindingEmailBody,
        string directDebitingRemindingEmailSubject,
        string directDebitingRemindingEmailBody,
        Guid userId,
        User user) : base(Guid.NewGuid())
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
        PhoneNumber = phoneNumber;
        Email = email;
        HomePage = homePage;
        AccountName = accountName;
        IBAN = iBAN;
        BIC = bIC;
        GID = gID;
        TaxIdentificationNumber = taxIdentificationNumber;
        InvoicePdfBody = invoicePdfBody;
        InvoiceEmailSubject = invoiceEmailSubject;
        InvoiceEmailBody = invoiceEmailBody;
        BillingRemindingEmailSubject = billingRemindingEmailSubject;
        BillingRemindingEmailBody = billingRemindingEmailBody;
        DirectDebitingRemindingEmailSubject = directDebitingRemindingEmailSubject;
        DirectDebitingRemindingEmailBody = directDebitingRemindingEmailBody;
        UserId = userId;
        User = user;
    }

    public string Street { get; private set; }

    public string City { get; private set; }

    public string ZipCode { get; private set; }

    public string PhoneNumber { get; private set; }

    public string Email { get; private set; }

    public string HomePage { get; private set; }

    public string AccountName { get; private set; }

    public string IBAN { get; private set; }

    public string BIC { get; private set; }

    public string GID { get; private set; }

    public string TaxIdentificationNumber {  get; private set; }

    public string InvoiceEmailSubject { get; private set; }

    public string InvoiceEmailBody { get; private set; }

    public string InvoicePdfBody { get; private set; }

    public string BillingRemindingEmailSubject { get; private set; }

    public string BillingRemindingEmailBody { get; private set; }

    public string DirectDebitingRemindingEmailSubject { get; private set; }

    public string DirectDebitingRemindingEmailBody { get; private set; }


    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public void Update(
        string street,
        string city,
        string zipCode,
        string phoneNumber,
        string email,
        string homePage,
        string accountName,
        string iBAN,
        string bIC,
        string gID,
        string taxIdentificationNumber,
        string invoicePdfBody,
        string invoiceEmailSubject,
        string invoiceEmailBody,
        string billingRemindingEmailSubject,
        string billingRemindingEmailBody,
        string directDebitingRemindingEmailSubject,
        string directDebitingRemindingEmailBody)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
        PhoneNumber = phoneNumber;
        Email = email;
        HomePage = homePage;
        AccountName = accountName;
        IBAN = iBAN;
        BIC = bIC;
        GID = gID;
        TaxIdentificationNumber = taxIdentificationNumber;
        InvoicePdfBody = invoicePdfBody;
        InvoiceEmailSubject = invoiceEmailSubject;
        InvoiceEmailBody = invoiceEmailBody;
        BillingRemindingEmailSubject = billingRemindingEmailSubject;
        BillingRemindingEmailBody = billingRemindingEmailBody;
        DirectDebitingRemindingEmailSubject = directDebitingRemindingEmailSubject;
        DirectDebitingRemindingEmailBody = directDebitingRemindingEmailBody;
    }
}
