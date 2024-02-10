using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Shared.Email;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(Email.MaximumLength)
                .WithError(DomainErrors.Email.TooLong)
            .Matches(Email.FormatPattern)
                .WithError(DomainErrors.Email.InvalidFormat);
    }
}
