using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Customers.Memberships.Institutions;
using BergerDb.Domain.Shared.DateRanges;

namespace BergerDb.Application.Customers.UpdateCustomerMembership;

public class UpdateCustomerMembershipCommandHandler : ICommandHandler<UpdateCustomerMembershipCommand>
{
    private readonly IMembershipRepository _membershipRepository;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerMembershipCommandHandler(IMembershipRepository membershipRepository, IUnitOfWork unitOfWork)
    {
        _membershipRepository = membershipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCustomerMembershipCommand request, CancellationToken cancellationToken)
    {
        Membership? membership = await _membershipRepository.GetMembershipByCustomerIdAsync(request.Id, cancellationToken);

        if (membership is null)
        {
            return Result.Failure(DomainErrors.Customer.NotFound);
        }

        var institutionResult = await Institution.CreateAsync(request.Institution);

        var invoiceDateResult = await InvoiceDateRange.CreateAsync(request.CurrentInvoiceSendedOn, request.LastInvoiceSendedOn);

        var creditDateResult = await InvoiceDateRange.CreateAsync(request.CurrentCreditReceivedOn, request.LastCreditReceivedOn);

        Result firstFailureOrSucces = Result.Concat(
            institutionResult, invoiceDateResult, creditDateResult);

        if (firstFailureOrSucces.IsFailure)
        {
            return firstFailureOrSucces;
        }

        membership.Update(
            request.PaymentType,
            request.MemberType,
            request.EntryType,
            institutionResult.Value,
            request.Amount,
            invoiceDateResult.Value,
            creditDateResult.Value,
            request.DunningSendedOn,
            request.TerminatedOn);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
