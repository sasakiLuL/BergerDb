using BergerDb.Shared.Entities;
using BergerDb.Shared.Results;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.ValueObjects.Names;

public record Name : ValueObject
{
    public static readonly int MaximumLength = 256;

    public static readonly Regex AllowedSymbolsPattern = NameRegex.GetRegex();

    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Name> Create(string value)
    {
        return Validate(
            new NameValidator(),
            new Name(value));
    }
}
