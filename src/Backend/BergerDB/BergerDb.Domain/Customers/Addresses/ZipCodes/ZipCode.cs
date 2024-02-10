using BergerDb.Domain.Core.Extensions;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.Addresses.PostalCodes;

public record ZipCode : ValueObject<string>
{
    public static readonly int MaximumLenght = 5;

    public static readonly Regex FormatPattern = new(@"^[\d]+$");

    private ZipCode(string Value) : base(Value) { }

    public static async Task<Result<ZipCode>> CreateAsync(string postalCode, CancellationToken token = default)
    {
        var postalCodeInstance = new ZipCode(postalCode);

        return (await new ZipCodeValidator().ValidateAsync(postalCodeInstance, token))
            .ToResult(postalCodeInstance);
    }
}
