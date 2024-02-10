using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Notations;

namespace BergerDb.Application.Customers.UpdateCustomerNotation;

public class UpdateCustomerNotationCommandHandler : ICommandHandler<UpdateCustomerNotationCommand>
{
    private ICustomerRepository _customerRepo;

    private IUnitOfWork _unitOfWork;

    public UpdateCustomerNotationCommandHandler(ICustomerRepository customerRepo, IUnitOfWork unitOfWork)
    {
        _customerRepo = customerRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCustomerNotationCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRepo.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(DomainErrors.Customer.NotFound);
        }

        var notationResult = await Notation.CreateAsync(request.Notation, cancellationToken);

        if (notationResult.IsFailure)
        {
            return notationResult;
        }

        customer.UpdateNotations(notationResult.Value);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
