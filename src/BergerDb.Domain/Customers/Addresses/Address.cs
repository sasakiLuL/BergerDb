using BergerDb.Domain.ValueObjects.Addresses;
using BergerDb.Shared.Entities;
using BergerDb.Shared.Results;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.Addresses;

public record Address : ValueObject
{
    public static readonly int MaximumLength = 255;

    public static readonly Regex AllowedSymbolsPattern = AddressRegex.GetRegex();

    private Address(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Address> Create(string value)
    {
        return Validate(new AddressValidator(), new Address(value));
    }
}