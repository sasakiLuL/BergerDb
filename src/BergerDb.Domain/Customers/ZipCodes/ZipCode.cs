using BergerDb.Domain.Customers.Addresses.ZipCodes;
using BergerDb.Shared.Entities;
using BergerDb.Shared.Results;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.ZipCodes;

public record ZipCode : ValueObject
{
    public static readonly int MaximumLength = 5;

    public static readonly Regex FormatPattern = ZipCodeRegex.GetRegex();

    private ZipCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ZipCode> Create(string value)
    {
        return Validate(
            new ZipCodeValidator(),
            new ZipCode(value));
    }
}
