using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Users.UserNames;

public class UserNameValidator : AbstractValidator<UserName>
{
    public UserNameValidator()
    {
        RuleFor(p => p.Value)
            .MinimumLength(UserName.MinimumLength).WithError(DomainErrors.UserName.TooShort)
            .MaximumLength(UserName.MaximumLength).WithError(DomainErrors.UserName.TooLong)
            .Matches(@"^[a-zA-Z0-9_]+$").WithError(DomainErrors.UserName.InvalidFormat);
    }
}
