using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Memberships;

namespace BergerDb.Application.Customers.CreateCustomer;

public record CreateCustomerCommand(
    string Prefix,
    string FirstName,
    string LastName,
    string Email,
    long PersonalId,
    string Notation,
    Sex Sex,
    DateTime RegistrationDate,
    string Street,
    string ZipCode,
    string City,
    MemberType MemberType,
    string Institution,
    EntryType EntryType,
    PaymentType PaymentType,
    decimal Amount,
    DateTime? CurrentInvoiceSendedOn,
    DateTime? LastInvoiceSendedOn,
    DateTime? CurrentCreditReceivedOn,
    DateTime? LastCreditReceivedOn,
    DateTime? TerminatedOn) : ICommand;
