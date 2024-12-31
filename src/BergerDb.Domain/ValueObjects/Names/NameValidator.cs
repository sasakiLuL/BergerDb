using BergerDb.Shared.Results;
using FluentValidation;

namespace BergerDb.Domain.ValueObjects.Names;

public class NameValidator : AbstractValidator<Name>
{
    public NameValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(Name.MaximumLength)
                .WithError(NameErrors.TooLong)
            .Matches(Name.AllowedSymbolsPattern)
                .WithError(NameErrors.InvalidFormat);
    }
}
