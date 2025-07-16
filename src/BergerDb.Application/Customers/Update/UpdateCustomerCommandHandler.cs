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

namespace BergerDb.Application.Customers.Update;

public class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateCustomerCommand>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound);
        }

        var prefixResult = Prefix.Create(request.Prefix);
        var firstNameResult = Name.Create(request.FirstName);
        var lastNameResult = Name.Create(request.LastName);
        var emailAddresResult = EmailAddress.Create(request.EmailAddress);
        var notationResult = Notation.Create(request.Notation);
        var streetResult = Address.Create(request.Street);
        var cityResult = Address.Create(request.City);
        var zipCodeResult = ZipCode.Create(request.ZipCode);
        var institutionResult = Name.Create(request.Institution);

        var validationResults = Result.AllFailuresOrSuccess(
            prefixResult,
            firstNameResult,
            lastNameResult,
            emailAddresResult,
            notationResult,
            streetResult,
            cityResult,
            zipCodeResult,
            institutionResult);

        if (validationResults.IsFailure)
        {
            return Result.Failure(validationResults.Errors);
        }

        customer.PersonalId = request.PersonalId;
        customer.Prefix = prefixResult.Value;
        customer.FirstName = firstNameResult.Value;
        customer.LastName = lastNameResult.Value;
        customer.Sex = request.Sex;
        customer.EmailAddress = emailAddresResult.Value;
        customer.RegisteredOnUtc = request.RegisteredOnUtc;
        customer.Notation = notationResult.Value;
        customer.Street = streetResult.Value;
        customer.City = cityResult.Value;
        customer.ZipCode = zipCodeResult.Value;
        customer.PaymentType = request.PaymentType;
        customer.MemberType = request.MemberType;
        customer.EntryType = request.EntryType;
        customer.SubscriptionCost = request.SubscriptionCost;
        customer.Institution = institutionResult.Value;

        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
