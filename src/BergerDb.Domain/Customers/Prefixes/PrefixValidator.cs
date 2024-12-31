using BergerDb.Shared.Results;
using FluentValidation;

namespace BergerDb.Domain.Customers.Prefixes;

public class PrefixValidator : AbstractValidator<Prefix>
{
    public PrefixValidator()
    {
        RuleFor(t => t.Value)
            .MaximumLength(Prefix.MaximumLength)
                .WithError(PrefixErrors.TooLong)
            .Matches(Prefix.AllowedSymbolsPattern)
                .WithError(PrefixErrors.InvalidFormat);
    }
}
