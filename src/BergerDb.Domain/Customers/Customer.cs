using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Names;
using BergerDb.Domain.Customers.Notations;
using BergerDb.Domain.Customers.Prefixes;
using BergerDb.Domain.Customers.ZipCodes;
using BergerDb.Domain.Emails.EmailAddresses;
using BergerDb.Domain.PaymentProcesses;
using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Customers;

public class Customer : Entity
{
    private readonly List<PaymentProcess> _paymentProcesses = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Customer() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Customer(
        Guid id, 
        long personalId, 
        Prefix prefix, 
        Name firstName, 
        Name lastName, 
        Sex sex, 
        EmailAddress emailAddress, 
        DateTime registeredOnUtc, 
        Notation notation, 
        Address street, 
        Address city, 
        ZipCode zipCode, 
        PaymentType paymentType, 
        MemberType memberType, 
        EntryType entryType, 
        decimal subscriptionCost, 
        Name institution) : base(id)
    {
        PersonalId = personalId;
        Prefix = prefix;
        FirstName = firstName;
        LastName = lastName;
        Sex = sex;
        EmailAddress = emailAddress;
        RegisteredOnUtc = registeredOnUtc;
        Notation = notation;
        Street = street;
        City = city;
        ZipCode = zipCode;
        PaymentType = paymentType;
        MemberType = memberType;
        EntryType = entryType;
        SubscriptionCost = subscriptionCost;
        Institution = institution;

        switch (PaymentType)
        {
            case PaymentType.Billing:
                _paymentProcesses.Add(new Billing(Guid.NewGuid(), id));
                break;

            case PaymentType.Debiting:
                _paymentProcesses.Add(new Debiting(Guid.NewGuid(), id));
                break;
        }
    }

    public long PersonalId { get; set; }

    public Prefix Prefix { get; set; }

    public Name FirstName { get; set; }

    public Name LastName { get; set; }

    public Sex Sex { get; set; }

    public EmailAddress EmailAddress { get; set; }

    public DateTime RegisteredOnUtc { get; set; }

    public DateTime? TerminatedOnUtc { get; set; }

    public Notation Notation { get; set; }

    public Address Street { get; set; }

    public Address City { get; set; }

    public ZipCode ZipCode { get; set; }
    
    public PaymentType PaymentType { get; set; }

    public MemberType MemberType { get; set; }

    public EntryType EntryType { get; set; }

    public decimal SubscriptionCost { get; set; }

    public Name Institution { get; set; }

    public PaymentProcess? CurrentPaymentProcess => _paymentProcesses.LastOrDefault();

    public IReadOnlyList<PaymentProcess> PaymentProcesses => _paymentProcesses.AsReadOnly();
}
