using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Shared.FirstNames;

public class FirstNameValidator : AbstractValidator<FirstName>
{
    public FirstNameValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(FirstName.MaximumLength)
                .WithError(DomainErrors.FirstName.TooLong)
            .Matches(FirstName.AllowedSymbolsPattern)
                .WithError(DomainErrors.FirstName.InvalidFormat);
    }
}
