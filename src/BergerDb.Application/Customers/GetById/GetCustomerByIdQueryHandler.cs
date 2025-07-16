using AutoMapper;
using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Domain.Customers;
using BergerDb.Shared.Results;

namespace BergerDb.Application.Customers.GetById;

public class GetCustomerByIdQueryHandler(
    ICustomerRepository customerRepository,
    IMapper mapper) : IQueryHandler<GetCustomerByIdQuery, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure<CustomerResponse>(CustomerErrors.NotFound);
        }

        return Result.Success(mapper.Map<CustomerResponse>(customer));
    }
}
