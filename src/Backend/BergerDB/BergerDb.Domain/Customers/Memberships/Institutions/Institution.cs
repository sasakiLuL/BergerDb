using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.Memberships.Institutions;

public record Institution : ValueObject<string>
{
    public static readonly int MaximumLength = 100;

    public static readonly Regex AllowedSymbolsPattern = new(@"^[\p{L}0-9 ,\.\/\\!@#$%&*+()_-]*$");

    private Institution(string Value) : base(Value) { }

    public static async Task<Result<Institution>> CreateAsync(string name, CancellationToken token = default)
    {
        Institution nameObjectInstance = new Institution(name);

        return (await new InstitutionValidator().ValidateAsync(nameObjectInstance, token))
            .ToResult(nameObjectInstance)!;
    }

}
