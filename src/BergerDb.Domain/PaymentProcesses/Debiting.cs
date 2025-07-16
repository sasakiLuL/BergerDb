using BergerDb.Domain.Customers;

namespace BergerDb.Domain.PaymentProcesses;

public class Debiting : PaymentProcess
{
    private Debiting() : base() { }

    public Debiting(
        Guid id,
        Guid customerId) : base(
            id, 
            PaymentType.Debiting,
            customerId)
    {
    }

    public override bool IsExpired()
    {
        return IsPending();
    }
}
