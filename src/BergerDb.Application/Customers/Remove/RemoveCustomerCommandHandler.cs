using BergerDb.Application.Abstractions.Data;
using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Domain.Customers;
using BergerDb.Shared.Results;

namespace BergerDb.Application.Customers.Remove;

public class RemoveCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveCustomerCommand>
{
    public async Task<Result> Handle(RemoveCustomerCommand request, CancellationToken token)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id, token);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound);
        }

        customerRepository.Delete(customer);

        await unitOfWork.CommitAsync(token);

        return Result.Success();
    }
}
