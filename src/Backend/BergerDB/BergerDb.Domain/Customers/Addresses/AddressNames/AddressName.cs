using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Shared.FirstNames;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.Addresses.AddressNames;

public record AddressName : ValueObject<string>
{
    public static readonly int MaximumLength = 100;

    public static readonly Regex AllowedSymbolsPattern = new(@"^[\p{L}0-9, \\\.\/-]*$");

    private AddressName(string Value) : base(Value) { }

    public static async Task<Result<AddressName>> CreateAsync(string name, CancellationToken token = default)
    {
        AddressName nameObjectInstance = new AddressName(name);

        return (await new AddressNameValidator().ValidateAsync(nameObjectInstance, token))
            .ToResult(nameObjectInstance)!;
    }

}
