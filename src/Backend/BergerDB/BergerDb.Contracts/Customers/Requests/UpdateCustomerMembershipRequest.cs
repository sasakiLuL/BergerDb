namespace BergerDb.Contracts.Customers.Requests;

public record UpdateCustomerMembershipRequest(
    int PaymentType,
    int MemberType,
    string Institution,
    int EntryType,
    decimal Amount,
    DateTime? CurrentInvoiceSendedOn,
    DateTime? LastInvoiceSendedOn,
    DateTime? CurrentCreditReceivedOn,
    DateTime? LastCreditReceivedOn,
    DateTime? DunningSendedOn,
    DateTime? TerminatedOn);