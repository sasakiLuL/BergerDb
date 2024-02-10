using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Customers.UpdateCustomerAddress;

public class UpdateCustomerAddressCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
{
    public UpdateCustomerAddressCommandValidator()
    {
        RuleFor(a => a.City).NotEmpty().WithError(ValidationErrors.Address.CityIsRequired);

        RuleFor(a => a.Street).NotEmpty().WithError(ValidationErrors.Address.StreetIsRequired);

        RuleFor(a => a.ZipCode).NotEmpty().WithError(ValidationErrors.Address.ZipCodeIsRequired);
    }
}
