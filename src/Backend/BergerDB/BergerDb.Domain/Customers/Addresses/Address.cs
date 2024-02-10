using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers.Addresses.AddressNames;
using BergerDb.Domain.Customers.Addresses.PostalCodes;

namespace BergerDb.Domain.Customers.Addresses;

public class Address : Entity
{
    private Address() : base(Guid.NewGuid()) { }

    public Address(
        AddressName street,
        ZipCode zipCode,
        AddressName city,
        Guid customerId,
        Customer customer) : base(Guid.NewGuid())
    {
        Street = street;
        ZipCode = zipCode;
        City = city;
        CustomerId = customerId;
        Customer = customer;
    }

    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; }

    public AddressName Street { get; private set; }

    public ZipCode ZipCode { get; private set; }

    public AddressName City { get; private set; }

    public void Update(AddressName street, ZipCode postalCode, AddressName city)
    {
        Street = street;
        ZipCode = postalCode;
        City = city;
    }
}
