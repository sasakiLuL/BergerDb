using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers.Notations;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.NameTitles;

public record Prefix : ValueObject<string>
{
    public static readonly int MaximumLength = 50;

    public static readonly Regex AllowedSymbolsPattern = new(@"^[\p{L}0-9, \\\.\/-]*$");

    public Prefix(string Value) : base(Value) { }

    public static async Task<Result<Prefix>> CreateAsync(string nameTitle, CancellationToken token = default)
    {
        var nameTitleInstance = new Prefix(nameTitle);

        return (await new PrefixValidator().ValidateAsync(nameTitleInstance, token))
            .ToResult(nameTitleInstance);
    }
}
