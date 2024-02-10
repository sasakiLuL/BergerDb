using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Customers.Addresses.PostalCodes;

public class ZipCodeValidator : AbstractValidator<ZipCode>
{
    public ZipCodeValidator()
    {
        RuleFor(c => c.Value)
            .MaximumLength(ZipCode.MaximumLenght).WithError(DomainErrors.ZipCode.TooLong)
            .Matches(ZipCode.FormatPattern).WithError(DomainErrors.ZipCode.InvalidFormat);
    }
}
