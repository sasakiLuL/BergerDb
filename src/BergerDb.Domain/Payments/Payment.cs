using BergerDb.Domain.PaymentProcesses;
using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Payments;

public class Payment : Entity<PaymentId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Payment() : base(new PaymentId(Guid.NewGuid())) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Payment(
        PaymentId id,
        decimal value,
        DateTime paidOnUtc,
        PaymentProcessId paymentProcessId) : base(id)
    {
        Value = value;
        PaidOnUtc = paidOnUtc;
        PaymentProcessId = paymentProcessId;
    }

    public decimal Value { get; set; }

    public DateTime PaidOnUtc { get; set; }

    public PaymentProcessId PaymentProcessId { get; set; }
}
