using BergerDb.Core.Results;
using FluentValidation;

namespace BergerDb.Domain.Users.Passwords;

public class PasswordValidator : AbstractValidator<Password>
{
    public PasswordValidator() : base()
    {
        RuleFor(p => p.Value)
            .MinimumLength(Password.MinimumLength)
                .WithError(PasswordErrors.TooShort)
            .MaximumLength(Password.MaximumLenght)
                .WithError(PasswordErrors.TooLong)
            .Matches(Password.FormatPattern)
                .WithError(PasswordErrors.InvalidFormat)
            .Matches(Password.MissingLetterPattern)
                .WithError(PasswordErrors.MissingLetter)
            .Matches(Password.MissingDigidPattern)
                .WithError(PasswordErrors.MissingDigit);
    }
}
