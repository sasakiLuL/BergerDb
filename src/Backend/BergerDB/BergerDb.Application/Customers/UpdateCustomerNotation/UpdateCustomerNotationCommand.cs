using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Customers.UpdateCustomerNotation;

public record UpdateCustomerNotationCommand(
    Guid Id,
    string Notation) : ICommand;
