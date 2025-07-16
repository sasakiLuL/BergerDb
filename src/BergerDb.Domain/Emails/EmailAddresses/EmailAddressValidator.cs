using BergerDb.Shared.Results;
using FluentValidation;

namespace BergerDb.Domain.Emails.EmailAddresses;

public class EmailAddressValidator : AbstractValidator<EmailAddress>
{
    public EmailAddressValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(EmailAddress.MaximumLength)
                .WithError(EmailAddressErrors.TooLong)
            .Matches(EmailAddress.FormatPattern)
                .WithError(EmailAddressErrors.InvalidFormat);
    }
}
