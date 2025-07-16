using BergerDb.Domain.Customers;
using BergerDb.UI.Models.PaymentProcesses;

namespace BergerDb.UI.Models.Customers;

public class CustomerModel
{
    public Guid Id { get; set; } = Guid.Empty;

    public long PersonalId { get; set; } = 0;

    public string Prefix { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Sex Sex { get; set; }

    public string EmailAddress { get; set; } = string.Empty;

    public DateTime RegisteredOnUtc { get; set; }

    public DateTime? TerminatedOnUtc { get; set; }

    public string Notation { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public PaymentType PaymentType { get; set; }

    public MemberType MemberType { get; set; }

    public EntryType EntryType { get; set; }

    public decimal SubscriptionCost { get; set; }

    public string Institution { get; set; } = string.Empty;

    public List<PaymentProcessModel> PaymentProcesses { get; set; } = [];
}
