using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Shared.DateRanges;

public class InvoiceDateRangeValidator : AbstractValidator<InvoiceDateRange>
{
    public InvoiceDateRangeValidator()
    {
        RuleFor(p => p.Current)
            .GreaterThanOrEqualTo(p => p.Last)
            .When(p => p.Last != null).WithError(DomainErrors.InvoiceDateRange.ValueError);
    }
}