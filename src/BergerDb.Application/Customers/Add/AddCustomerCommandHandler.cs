using BergerDb.Application.Abstractions.Data;
using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Names;
using BergerDb.Domain.Customers.Notations;
using BergerDb.Domain.Customers.Prefixes;
using BergerDb.Domain.Customers.ZipCodes;
using BergerDb.Domain.Emails.EmailAddresses;
using BergerDb.Shared.Results;

namespace BergerDb.Application.Customers.Add;

public class AddCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddCustomerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddCustomerCommand request, CancellationToken token)
    {
        var customerId = Guid.NewGuid();

        var prefixResult = Prefix.Create(request.Prefix);

        var firstNameResult = Name.Create(request.FirstName);

        var lastNameResult = Name.Create(request.LastName);

        var emailAddressResult = EmailAddress.Create(request.EmailAddress);

        var notationResult = Notation.Create(request.Notation);

        var streetResult = Address.Create(request.Street);

        var cityResult = Address.Create(request.City);

        var zipCodeResult = ZipCode.Create(request.ZipCode);

        var institutionResult = Name.Create(request.Institution);

        var validationResult = Result.AllFailuresOrSuccess(
            prefixResult,
            firstNameResult,
            lastNameResult,
            emailAddressResult,
            notationResult,
            streetResult,
            cityResult,
            zipCodeResult,
            institutionResult);

        if (validationResult.IsFailure)
        {
            return Result.Failure<Guid>(validationResult.Errors);
        }

        var customer = new Customer(
            customerId,
            request.PersonalId,
            prefixResult.Value,
            firstNameResult.Value,
            lastNameResult.Value,
            request.Sex,
            emailAddressResult.Value,
            request.RegisteredOnUtc,
            notationResult.Value,
            streetResult.Value,
            cityResult.Value,
            zipCodeResult.Value,
            request.PaymentType,
            request.MemberType,
            request.EntryType,
            request.SubscriptionCost,
            institutionResult.Value);

        customerRepository.Add(customer);

        await unitOfWork.CommitAsync(token);

        return Result.Success(customerId);
    }
}
