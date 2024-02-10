using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Customers.Responses;

namespace BergerDb.Application.Customers.GetCustomer;

public record GetCustomerQuery(Guid Id) : IQuery<CustomerResponse>;
