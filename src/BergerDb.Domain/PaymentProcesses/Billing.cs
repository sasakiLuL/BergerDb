using BergerDb.Domain.Customers;
using BergerDb.Domain.Emails;
using BergerDb.Shared.Time;

namespace BergerDb.Domain.PaymentProcesses;

public class Billing : PaymentProcess
{
    public const int TimeToPayInDays = 14;

    private Billing() : base() { }

    public Billing(
        PaymentProcessId id,
        CustomerId customerId) : base(
            id, 
            PaymentType.Billing,
            customerId)
    {
    }

    public override bool IsExpired()
    {
        if (PaymentId is not null)
        {
            return false;
        }

        var lastInvoice = _emails
            .Where(e => e.EmailType is EmailType.Invoice)
            .MaxBy(e => e.SentOnUtc);

        if (lastInvoice is null)
        {
            return false;
        }

        return (lastInvoice.SentOnUtc - SystemTimeProvider.UtcNow).Days > TimeToPayInDays;
    }
}
