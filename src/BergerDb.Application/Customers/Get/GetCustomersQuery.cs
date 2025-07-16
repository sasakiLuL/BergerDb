using BergerDb.Application.Abstractions.Messaging;

namespace BergerDb.Application.Customers.Get;

public record GetCustomersQuery(GetCustomersQueryFilters Filters) : IQuery<List<CustomerResponse>>;
