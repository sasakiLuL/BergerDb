using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Customers.Addresses.AddressNames;
using FluentValidation;

namespace BergerDb.Domain.Shared.FirstNames;

public class AddressNameValidator : AbstractValidator<AddressName>
{
    public AddressNameValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(AddressName.MaximumLength).WithError(DomainErrors.AddressName.TooLong)
            .Matches(AddressName.AllowedSymbolsPattern).WithError(DomainErrors.AddressName.InvalidFormat);
    }
}



