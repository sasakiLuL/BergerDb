using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Users.Passwords;

public class PasswordValidator : AbstractValidator<Password>
{
    public PasswordValidator()
    {
        RuleFor(p => p.Value)
            .MinimumLength(Password.MinimumLength).WithError(DomainErrors.Password.TooShort)
            .MaximumLength(Password.MaximumLenght).WithError(DomainErrors.Password.TooLong)
            .Matches(Password.FormatPattern).WithError(DomainErrors.Password.InvalidFormat)
            .Matches(Password.MissingLetterPattern).WithError(DomainErrors.Password.MissingLetter)
            .Matches(Password.MissingDigidPattern).WithError(DomainErrors.Password.MissingDigit);
    }
}
