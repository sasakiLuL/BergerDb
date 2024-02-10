using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Common;
using BergerDb.Contracts.Customers.Requests.Filtering;
using BergerDb.Contracts.Customers.Responses;

namespace BergerDb.Application.Customers.GetCustomers;

public record GetCustomersQuery(GetCustomersFilters Filters) : IQuery<PagedList<CustomerResponse>>;
