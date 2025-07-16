using BergerDb.Application.Payments;
using BergerDb.Domain.Customers;

namespace BergerDb.Application.Customers;

public record CustomerResponse(
    Guid Id,
    long PersonalId,
    string Prefix,
    string FirstName,
    string LastName,
    Sex Sex,
    string EmailAddress,
    DateTime RegisteredOnUtc,
    DateTime? TerminatedOnUtc,
    string Notation,
    string Street,
    string City,
    string ZipCode,
    PaymentType PaymentType,
    MemberType MemberType,
    EntryType EntryType,
    decimal SubscriptionCost,
    string Institution);
