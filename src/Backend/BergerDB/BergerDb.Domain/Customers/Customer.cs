using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Customers.NameTitles;
using BergerDb.Domain.Customers.Notations;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;

namespace BergerDb.Domain.Customers;

public sealed class Customer : Entity
{
    private Customer() : base(Guid.NewGuid()) {}

    public Customer(
        Prefix prefix,
        FirstName firstName,
        LastName lastName,
        Sex sex,
        Email email,
        Notation notation,
        long personalId,
        DateTime registrationDate,
        Address? address,
        Membership? membership) : base(Guid.NewGuid())
    {
        Prefix = prefix;
        Notation = notation;
        PersonalId = personalId;
        Sex = sex;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        RegistrationDate = registrationDate;
        Address = address;
        Membership = membership;
    }

    public Address? Address { get; private set; }

    public Membership? Membership { get; private set; }

    public Sex Sex { get; private set; }

    public Prefix Prefix { get; private set; }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public DateTime RegistrationDate { get; private set; }

    public Notation Notation { get; private set; }

    public long PersonalId { get; private set; }

    public void UpdatePersonalInfo(
        Prefix title, 
        FirstName firstName, 
        LastName lastName, 
        Email email, 
        long personalId,
        Sex sex,
        DateTime registrationDate)
    {
        Prefix = title;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Sex = sex;
        PersonalId = personalId;
        RegistrationDate = registrationDate;
    }

    public void UpdateNotations(Notation notation)
    { 
        Notation = notation;
    }
}
