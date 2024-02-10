using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.NameTitles;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;

namespace BergerDb.Application.Customers.UpdateCustomerPersomalInfo;

public class UpdateCustomerPersomalInfoCommandHandler : ICommandHandler<UpdateCustomerPersomalInfoCommand>
{
    private readonly ICustomerRepository _customerRepo;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerPersomalInfoCommandHandler(ICustomerRepository customerRepo, IUnitOfWork unitOfWork)
    {
        _customerRepo = customerRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCustomerPersomalInfoCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRepo.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(DomainErrors.Customer.NotFound);
        }

        var titleNameResult = await Prefix.CreateAsync(request.Prefix);

        var firstNameResult = await FirstName.CreateAsync(request.FirstName);

        var lastNameResult = await LastName.CreateAsync(request.LastName);

        var emailResult = await Email.CreateAsync(request.Email);

        Result firstFailureOrSucces = Result.Concat(
            titleNameResult,
            firstNameResult,
            lastNameResult,
            emailResult);

        if (firstFailureOrSucces.IsFailure)
        {
            return firstFailureOrSucces;
        }

        customer.UpdatePersonalInfo(
            titleNameResult.Value!,
            firstNameResult.Value!,
            lastNameResult.Value!,
            emailResult.Value,
            request.PersonalId,
            request.Sex,
            request.RegistrationDate);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
