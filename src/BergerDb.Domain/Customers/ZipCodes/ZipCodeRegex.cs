using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.Addresses.ZipCodes;

internal static partial class ZipCodeRegex
{
    [GeneratedRegex(@"^[\d]+$", RegexOptions.Compiled)]
    internal static partial Regex GetRegex();
}
