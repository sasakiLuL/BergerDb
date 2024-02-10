using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Customers.UpdateCustomerAddress;

public record UpdateCustomerAddressCommand(
    Guid Id,
    string Street,
    string ZipCode,
    string City) : ICommand;
