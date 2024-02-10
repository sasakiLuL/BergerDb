using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Customers.CreateCustomer;

public class CreateCustomerCommandValidation : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidation()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithError(ValidationErrors.Customer.FirstNameIsRequired);

        RuleFor(c => c.LastName)
            .NotEmpty().WithError(ValidationErrors.Customer.LastNameIsRequired);

        RuleFor(c => c.Email)
            .NotEmpty().WithError(ValidationErrors.Customer.EmailIsRequired);

        RuleFor(c => c.Sex)
            .IsInEnum().WithError(ValidationErrors.Customer.WrongSexValue);
            
        RuleFor(c => c.Street)
            .NotEmpty().WithError(ValidationErrors.Address.StreetIsRequired);

        RuleFor(c => c.ZipCode)
            .NotEmpty().WithError(ValidationErrors.Address.ZipCodeIsRequired);

        RuleFor(c => c.City)
            .NotEmpty().WithError(ValidationErrors.Address.CityIsRequired);

        RuleFor(c => c.MemberType)
            .IsInEnum().WithError(ValidationErrors.Membership.WrongMemberTypeValue);

        RuleFor(c => c.EntryType)
            .IsInEnum().WithError(ValidationErrors.Membership.WrongEntryTypeValue);

        RuleFor(c => c.PaymentType)
            .IsInEnum().WithError(ValidationErrors.Membership.WrongPaymentMethodValue);

        RuleFor(c => c.Amount)
            .GreaterThanOrEqualTo(0).WithError(ValidationErrors.Membership.WrongMoneyValue);
    }
}
