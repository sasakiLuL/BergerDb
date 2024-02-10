using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Customers.Responses;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using Mapster;

namespace BergerDb.Application.Customers.GetCustomer;

public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly ICustomerResponseLinksService _customerLinkResponseService;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository, ICustomerResponseLinksService linkService)
    {
        _customerRepository = customerRepository;
        _customerLinkResponseService = linkService;
    }

    public async Task<Result<CustomerResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)   
        {
            return Result.Failure<CustomerResponse>(DomainErrors.Customer.NotFound);
        }

        customer.Membership!.UpdateStatus();

        var response = customer.Adapt<CustomerResponse>();

        _customerLinkResponseService.GenerateLinks(response);

        return Result.Success(response);
    }
}
