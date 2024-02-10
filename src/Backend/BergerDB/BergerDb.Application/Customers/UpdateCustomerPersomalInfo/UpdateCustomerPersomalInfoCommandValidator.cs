using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Customers.UpdateCustomerPersomalInfo;

public class UpdateCustomerPersomalInfoCommandValidator : AbstractValidator<UpdateCustomerPersomalInfoCommand>
{
    public UpdateCustomerPersomalInfoCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithError(ValidationErrors.Customer.FirstNameIsRequired);

        RuleFor(c => c.LastName)
            .NotEmpty().WithError(ValidationErrors.Customer.LastNameIsRequired);

        RuleFor(c => c.Email)
            .NotEmpty().WithError(ValidationErrors.Customer.EmailIsRequired);

        RuleFor(c => c.Sex)
            .IsInEnum().WithError(ValidationErrors.Customer.WrongSexValue);
    }
}
