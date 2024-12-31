using BergerDb.Shared.Results;
using FluentValidation;

namespace BergerDb.Domain.Customers.ZipCodes;

public class ZipCodeValidator : AbstractValidator<ZipCode>
{
    public ZipCodeValidator()
    {
        RuleFor(c => c.Value)
            .MaximumLength(ZipCode.MaximumLength)
                .WithError(ZipCodeErrors.TooLong)
            .Matches(ZipCode.FormatPattern)
                .WithError(ZipCodeErrors.InvalidFormat);
    }
}
