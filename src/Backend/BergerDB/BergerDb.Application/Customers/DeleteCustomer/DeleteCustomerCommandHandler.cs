using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Memberships;

namespace BergerDb.Application.Customers.DeleteCustomer;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IAddressRepository _addressRepository;

    private readonly IMembershipRepository _membershipRepository;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IAddressRepository addressRepository, IMembershipRepository membershipRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _addressRepository = addressRepository;
        _membershipRepository = membershipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken = default)
    {
        Customer? customer = await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(DomainErrors.Customer.NotFound);
        }

        _addressRepository.Delete(customer.Address!);

        _membershipRepository.Delete(customer.Membership!);

        _customerRepository.Delete(customer);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
