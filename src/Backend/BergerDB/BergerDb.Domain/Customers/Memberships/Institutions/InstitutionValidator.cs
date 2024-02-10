using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Customers.Memberships.Institutions;

public class InstitutionValidator : AbstractValidator<Institution>
{
    public InstitutionValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(Institution.MaximumLength).WithError(DomainErrors.Institution.TooLong)
            .Matches(Institution.AllowedSymbolsPattern).WithError(DomainErrors.Institution.InvalidFormat);
    }
}
