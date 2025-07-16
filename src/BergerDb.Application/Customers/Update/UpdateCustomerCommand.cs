using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Domain.Customers;

namespace BergerDb.Application.Customers.Update;

public record UpdateCustomerCommand(
    Guid Id,
    long PersonalId,
    string Prefix,
    string FirstName,
    string LastName,
    Sex Sex,
    string EmailAddress,
    DateTime RegisteredOnUtc,
    string Notation,
    string Street,
    string City,
    string ZipCode,
    PaymentType PaymentType,
    MemberType MemberType,
    EntryType EntryType,
    decimal SubscriptionCost,
    string Institution) : ICommand;