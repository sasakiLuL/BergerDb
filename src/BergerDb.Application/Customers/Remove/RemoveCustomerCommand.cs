using BergerDb.Application.Abstractions.Messaging;

namespace BergerDb.Application.Customers.Remove;

public record RemoveCustomerCommand(Guid Id) : ICommand;
