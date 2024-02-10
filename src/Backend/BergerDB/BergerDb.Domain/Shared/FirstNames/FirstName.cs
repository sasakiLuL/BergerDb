using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Shared.FirstNames;

public record FirstName : ValueObject<string>
{
    public static readonly int MaximumLength = 100;

    public static readonly Regex AllowedSymbolsPattern = new(@"^[\p{L}0-9 ,\.\/\\!@#$%&*+()_-]*$");

    private FirstName(string Value) : base(Value) { }

    public static async Task<Result<FirstName?>> CreateAsync(string? name, CancellationToken token = default)
    {
        if (name is null)
        {
            return Result.Success<FirstName?>(null);
        }

        FirstName nameObjectInstance = new FirstName(name);

        return (await new FirstNameValidator().ValidateAsync(nameObjectInstance, token))
            .ToResult(nameObjectInstance)!;
    }

}
