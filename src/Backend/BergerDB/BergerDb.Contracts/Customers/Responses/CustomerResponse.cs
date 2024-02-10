using BergerDb.Contracts.Common;

namespace BergerDb.Contracts.Customers.Responses;

public record CustomerResponse(
    Guid Id,
    string Prefix,
    string FirstName,
    string LastName,
    string Email,
    string Notation,
    long PersonalId,
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
    bool IsRecivedInvoice,
    bool IsRecivedDunning,
    bool IsDebtor,
    DateTime? CurrentInvoiceSendedOn,
    DateTime? LastInvoiceSendedOn,
    DateTime? CurrentCreditReceivedOn,
    DateTime? LastCreditReceivedOn,
    DateTime? DunningSendedOn,
    DateTime? TerminatedOn)
{
    public List<Link> Links { get; } = [];
}