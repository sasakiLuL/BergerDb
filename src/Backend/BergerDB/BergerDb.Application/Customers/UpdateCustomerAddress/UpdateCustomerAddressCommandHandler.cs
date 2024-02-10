using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Addresses.AddressNames;
using BergerDb.Domain.Customers.Addresses.PostalCodes;

namespace BergerDb.Application.Customers.UpdateCustomerAddress;

public class UpdateCustomerAddressCommandHandler : ICommandHandler<UpdateCustomerAddressCommand>
{
    private IAddressRepository _addressRepo;

    private IUnitOfWork _unitOfWork;

    public UpdateCustomerAddressCommandHandler(IAddressRepository addressRepo, IUnitOfWork unitOfWork)
    {
        _addressRepo = addressRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        Address? address = await _addressRepo.GetAddressByCustomerIdAsync(request.Id, cancellationToken);

        if (address is null)
        {
            return Result.Failure(DomainErrors.Customer.NotFound);
        }

        var streetResult = await AddressName.CreateAsync(request.Street);

        var postalCodeResult = await ZipCode.CreateAsync(request.ZipCode);

        var cityResult = await AddressName.CreateAsync(request.City);

        Result firstFailureOrSucces = Result.Concat(
            streetResult,
            postalCodeResult,
            cityResult);

        if (firstFailureOrSucces.IsFailure)
        {
            return firstFailureOrSucces;
        }

        address.Update(streetResult.Value, postalCodeResult.Value, cityResult.Value);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
