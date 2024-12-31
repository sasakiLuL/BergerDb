using BergerDb.Domain.Customers;
using BergerDb.Domain.Emails;
using BergerDb.Domain.Payments;
using BergerDb.Shared.Entities;
using BergerDb.Shared.Results;

namespace BergerDb.Domain.PaymentProcesses;

public abstract class PaymentProcess : Entity<PaymentProcessId>
{
    protected readonly List<Email> _emails = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected PaymentProcess() : base(new PaymentProcessId(Guid.NewGuid())) {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    protected PaymentProcess(
        PaymentProcessId id,
        PaymentType paymentType,
        CustomerId customerId) : base(id)
    {
        PaymentType = paymentType;
        CustomerId = customerId;
    }

    public PaymentType PaymentType { get; init; }

    public PaymentId? PaymentId { get; protected set; }

    public CustomerId CustomerId { get; protected set; }

    public virtual Result SendEmail(Email email)
    {
        _emails.Add(email);

        return Result.Success();
    }

    public virtual Result Finish(PaymentId paymentId)
    {
        PaymentId = paymentId;

        return Result.Success();
    }

    public virtual Result Cancel()
    {
        PaymentId = null;

        return Result.Success();
    }

    public virtual bool IsPending()
    {
        return PaymentId is null;
    }

    public virtual bool IsMade()
    {
        return !IsPending();
    }

    public abstract bool IsExpired();

    public IReadOnlyList<Email> Emails => _emails.AsReadOnly();
}
