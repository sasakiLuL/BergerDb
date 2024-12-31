using BergerDb.Shared.Results;
using FluentValidation;

namespace BergerDb.Domain.ValueObjects.Addresses;

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
