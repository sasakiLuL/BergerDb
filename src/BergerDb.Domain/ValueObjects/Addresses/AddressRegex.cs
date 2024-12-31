using System.Text.RegularExpressions;

namespace BergerDb.Domain.ValueObjects.Addresses;

internal static partial class AddressRegex
{
    [GeneratedRegex(@"^[\p{L}0-9,;'#№ &\\\.\/-]*$", RegexOptions.Compiled)]
    internal static partial Regex GetRegex();
}
