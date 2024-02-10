using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Customers.DeleteCustomer;

public record DeleteCustomerCommand(
    Guid Id) : ICommand;
