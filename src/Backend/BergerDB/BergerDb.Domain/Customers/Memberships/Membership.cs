using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Customers.Memberships.Institutions;
using BergerDb.Domain.Shared.DateRanges;
using System.Data;

namespace BergerDb.Domain.Customers.Memberships;

public class Membership : Entity
{
    private Membership() : base(Guid.NewGuid()) { }

    public Membership(
        MemberType memberType,
        PaymentType paymentType,
        EntryType entryType,
        Institution institution,
        decimal amount,
        InvoiceDateRange invoiceSendedOn,
        InvoiceDateRange creditReceivedOn,
        DateTime? terminatedOn,
        Guid customerId,
        Customer customer) : base(Guid.NewGuid())
    {
        MemberType = memberType;
        Institution = institution;
        EntryType = entryType;
        PaymentType = paymentType;
        Amount = amount;
        InvoiceSendedOn = invoiceSendedOn;
        CreditReceivedOn = creditReceivedOn;
        DunningSendedOn = null;
        TerminatedOn = terminatedOn;
        CustomerId = customerId;
        Customer = customer;
        UpdateStatus();
    }

    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; }

    public MemberType MemberType { get; private set; }

    public Institution Institution { get; private set; }

    public EntryType EntryType { get; private set; }

    public PaymentType PaymentType { get; private set; }

    public decimal Amount { get; private set; }

    public InvoiceDateRange InvoiceSendedOn { get; private set; }

    public InvoiceDateRange CreditReceivedOn { get; private set; }

    public DateTime? DunningSendedOn { get; private set; }

    public DateTime? TerminatedOn { get; private set; }

    public bool IsRecivedInvoice { get; private set; }

    public bool IsRecivedDunning { get; private set; }

    public bool IsDebtor { get; private set; }

    public void Update(
        PaymentType paymentType,
        MemberType memberType,
        EntryType entryType,
        Institution institution,
        decimal amount,
        InvoiceDateRange invoiceSendedOn,
        InvoiceDateRange creditReceivedOn,
        DateTime? dunningSendedOn,
        DateTime? terminatedOn)
    {
        PaymentType = paymentType;
        MemberType = memberType;
        EntryType = entryType;
        Institution = institution;
        Amount = amount;
        InvoiceSendedOn = invoiceSendedOn;
        CreditReceivedOn = creditReceivedOn;
        TerminatedOn = terminatedOn;
        DunningSendedOn = dunningSendedOn;
        UpdateStatus();
}

    public void UpdateStatus()
    {
        IsRecivedInvoice = InvoiceSendedOn.Current.HasValue;
        IsRecivedDunning = DunningSendedOn.HasValue;
        IsDebtor = (PaymentType == PaymentType.Billing && 
            InvoiceSendedOn.Current.HasValue &&
            (DateTime.UtcNow - InvoiceSendedOn.Current).Value.Days > 14 &&
            !CreditReceivedOn.Current.HasValue)
            || 
            (PaymentType == PaymentType.DirectDebiting && DunningSendedOn.HasValue);
    }

    public void InvoiceIsSended()
    {
        InvoiceSendedOn = InvoiceDateRange.CreateAsync(DateTime.UtcNow, InvoiceSendedOn.Last).Result.Value;
        IsRecivedInvoice = true;
    }

    public void DunningIsSended()
    {
        DunningSendedOn = DateTime.UtcNow;
        IsRecivedDunning = true;
    }
}
