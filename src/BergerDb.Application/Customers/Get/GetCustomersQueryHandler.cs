using AutoMapper;
using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Domain.Customers;
using BergerDb.Shared.Results;

namespace BergerDb.Application.Customers.Get;

public class GetCustomersQueryHandler(
    ICustomerRepository customerRepository,
    IMapper mapper) : IQueryHandler<GetCustomersQuery, List<CustomerResponse>>
{
    public async Task<Result<List<CustomerResponse>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await customerRepository.GetQueryableAsync(
            query => query
                .Skip((request.Filters.Page - 1) * request.Filters.PageSize)
                .Take(request.Filters.PageSize), 
            cancellationToken);

        List<CustomerResponse> customersResponses = [];

        customersResponses = customers.Select(mapper.Map<CustomerResponse>).ToList();

        return Result.Success(customersResponses);
    }
}
