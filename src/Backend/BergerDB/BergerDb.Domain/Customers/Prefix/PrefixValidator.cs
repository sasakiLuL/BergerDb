using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Customers.NameTitles;

public class PrefixValidator : AbstractValidator<Prefix>
{
    public PrefixValidator()
    {
        RuleFor(t => t.Value)
            .MaximumLength(Prefix.MaximumLength)
                .WithError(DomainErrors.Prefix.TooLong)
            .Matches(Prefix.AllowedSymbolsPattern)
                .WithError(DomainErrors.Prefix.InvalidFormat);
    }
}
