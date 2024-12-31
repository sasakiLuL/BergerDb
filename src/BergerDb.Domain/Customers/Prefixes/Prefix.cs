using BergerDb.Shared.Entities;
using BergerDb.Shared.Results;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.Prefixes;

public record Prefix(string Value) : ValueObject
{
    public static readonly int MaximumLength = 50;

    public static readonly Regex AllowedSymbolsPattern = new(@"^[\p{L}0-9, \\\.\/-]*$");

    public static Result<Prefix> Create(string value)
    {
        return Validate(
            new PrefixValidator(),
            new Prefix(value));
    }
}
