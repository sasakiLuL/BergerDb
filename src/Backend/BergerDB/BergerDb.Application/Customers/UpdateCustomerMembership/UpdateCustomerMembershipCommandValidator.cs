using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Customers.UpdateCustomerMembership;

public class UpdateCustomerMembershipCommandValidator : AbstractValidator<UpdateCustomerMembershipCommand>
{
    public UpdateCustomerMembershipCommandValidator()
    {
        RuleFor(c => c.MemberType)
            .IsInEnum().WithError(ValidationErrors.Membership.WrongMemberTypeValue);

        RuleFor(c => c.EntryType)
            .IsInEnum().WithError(ValidationErrors.Membership.WrongEntryTypeValue);

        RuleFor(c => c.Amount)
            .GreaterThanOrEqualTo(0).WithError(ValidationErrors.Membership.WrongMoneyValue);
    }
}
