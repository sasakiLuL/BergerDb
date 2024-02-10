using BergerDb.Contracts.Customers.Requests.Filtering;

namespace BergerDb.Contracts.Customers.Requests;

public record GetAllCustomersRequest(
    GetCustomersFilters Filters);
