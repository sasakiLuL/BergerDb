using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Customers;

namespace BergerDb.Application.Customers.UpdateCustomerPersomalInfo;

public record UpdateCustomerPersomalInfoCommand(
    Guid Id,
    string Prefix,
    string FirstName,
    string LastName,
    string Email,
    long PersonalId,
    Sex Sex,
    DateTime RegistrationDate) : ICommand;
