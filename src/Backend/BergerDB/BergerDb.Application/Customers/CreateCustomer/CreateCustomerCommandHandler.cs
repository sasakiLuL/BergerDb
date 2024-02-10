using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Addresses.AddressNames;
using BergerDb.Domain.Customers.Addresses.PostalCodes;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Customers.Memberships.Institutions;
using BergerDb.Domain.Customers.NameTitles;
using BergerDb.Domain.Customers.Notations;
using BergerDb.Domain.Shared.DateRanges;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;

namespace BergerDb.Application.Customers.CreateCustomer;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
    private readonly IAddressRepository _addressRepo;

    private readonly ICustomerRepository _customerRepo;

    private readonly IMembershipRepository _membershipRepo;

    private readonly IUnitOfWork _unitOfWork;
    public CreateCustomerCommandHandler(
        IAddressRepository addressRepo,
        ICustomerRepository customerRepo,
        IMembershipRepository membershipRepo,
        IUnitOfWork unitOfWork)
    {
        _addressRepo = addressRepo;
        _membershipRepo = membershipRepo;
        _customerRepo = customerRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var prefixResult = await Prefix.CreateAsync(request.Prefix, cancellationToken);

        var firstNameResult = await FirstName.CreateAsync(request.FirstName, cancellationToken);

        var lastNameResult = await LastName.CreateAsync(request.LastName, cancellationToken);

        var emailResult = await Email.CreateAsync(request.Email, cancellationToken);

        var notationResult = await Notation.CreateAsync(request.Notation, cancellationToken);

        var streetResult = await AddressName.CreateAsync(request.Street, cancellationToken);

        var zipCodeResult = await ZipCode.CreateAsync(request.ZipCode, cancellationToken);

        var cityResult = await AddressName.CreateAsync(request.City, cancellationToken);

        var institutionResult = await Institution.CreateAsync(request.Institution, cancellationToken);

        var invoidDateResult = await InvoiceDateRange.CreateAsync(request.CurrentInvoiceSendedOn, request.LastInvoiceSendedOn, cancellationToken);

        var creditDateResult = await InvoiceDateRange.CreateAsync(request.CurrentCreditReceivedOn, request.LastCreditReceivedOn, cancellationToken);

        Result firstFailureOrSucces = Result.Concat(
            prefixResult, 
            firstNameResult, 
            lastNameResult, 
            emailResult, 
            notationResult, 
            streetResult, 
            zipCodeResult,
            cityResult, 
            institutionResult, 
            invoidDateResult, 
            creditDateResult);

        if (firstFailureOrSucces.IsFailure)
        {
            return firstFailureOrSucces;
        }

        Customer customer = new(
            prefixResult.Value!,
            firstNameResult.Value!,
            lastNameResult.Value!, 
            request.Sex,
            emailResult.Value, 
            notationResult.Value!, 
            request.PersonalId, 
            request.RegistrationDate,
            null, 
            null);

        _customerRepo.Add(customer);

        Address address = new(
            streetResult.Value, 
            zipCodeResult.Value, 
            cityResult.Value,
            customer.Id, 
            customer);

        Membership membership = new(
            request.MemberType, 
            request.PaymentType, 
            request.EntryType, 
            institutionResult.Value, 
            request.Amount,
            invoidDateResult.Value,
            creditDateResult.Value,
            request.TerminatedOn,
            customer.Id, 
            customer);

        _addressRepo.Add(address);

        _membershipRepo.Add(membership);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
