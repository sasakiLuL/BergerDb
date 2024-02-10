using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Shared.LastNames;

public class LastNameValidator : AbstractValidator<LastName>
{
    public LastNameValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(LastName.MaximumLength)
                .WithError(DomainErrors.LastName.TooLong)
            .Matches(LastName.AllowedSymbolsPattern)
                .WithError(DomainErrors.LastName.InvalidFormat);
    }
}
