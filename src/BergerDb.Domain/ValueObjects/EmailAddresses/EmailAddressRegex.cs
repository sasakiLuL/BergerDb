using System.Text.RegularExpressions;

namespace BergerDb.Domain.ValueObjects.EmailAddresses;

internal static partial class EmailAddressRegex
{
    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled)]
    internal static partial Regex GetRegex();
}
