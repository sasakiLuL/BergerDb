using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Customers.Memberships;

namespace BergerDb.Application.Customers.UpdateCustomerMembership;

public record UpdateCustomerMembershipCommand(
    Guid Id,
    PaymentType PaymentType,
    MemberType MemberType,
    string Institution,
    EntryType EntryType,
    decimal Amount,
    DateTime? CurrentInvoiceSendedOn,
    DateTime? LastInvoiceSendedOn,
    DateTime? CurrentCreditReceivedOn,
    DateTime? LastCreditReceivedOn,
    DateTime? DunningSendedOn,
    DateTime? TerminatedOn) : ICommand;