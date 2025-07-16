using BergerDb.Application.Abstractions.Messaging;

namespace BergerDb.Application.Customers.GetById;

public record GetCustomerByIdQuery(
    Guid Id) : IQuery<CustomerResponse>;
