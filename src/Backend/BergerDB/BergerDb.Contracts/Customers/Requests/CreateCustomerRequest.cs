namespace BergerDb.Contracts.Customers.Requests;

public record CreateCustomerRequest(
    string Prefix,
    string FirstName,
    string LastName,
    string Email,
    long PersonalId,
    string Notation,
    int Sex,
    DateTime RegistrationDate,
    string Street,
    string ZipCode,
    string City,
    int MemberType,
    string Institution,
    int EntryType,
    int PaymentType,
    decimal Amount,
    DateTime? CurrentInvoiceSendedOn,
    DateTime? LastInvoiceSendedOn,
    DateTime? CurrentCreditReceivedOn,
    DateTime? LastCreditReceivedOn,
    DateTime? TerminatedOn);