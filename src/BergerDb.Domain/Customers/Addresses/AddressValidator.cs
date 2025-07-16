using BergerDb.Shared.Results;
using FluentValidation;

namespace BergerDb.Domain.Customers.Addresses;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(p => p.Value)
            .MaximumLength(Address.MaximumLength)
                .WithError(AddressErrors.TooLong)
            .Matches(Address.AllowedSymbolsPattern)
                .WithError(AddressErrors.InvalidFormat);
    }
}
