using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Shared.LastNames;

public record LastName : ValueObject<string>
{
    public static readonly int MaximumLength = 100;

    public static readonly Regex AllowedSymbolsPattern = new(@"^[\p{L}0-9 ,\.\/\\!@#$%&*+()_-]*$");

    private LastName(string Value) : base(Value) { }

    public static async Task<Result<LastName?>> CreateAsync(string? name, CancellationToken token = default)
    {
        if (name is null)
        {
            return Result.Success<LastName?>(null);
        }

        LastName nameObjectInstance = new LastName(name);

        return (await new LastNameValidator().ValidateAsync(nameObjectInstance, token))
            .ToResult(nameObjectInstance)!;
    }
}
