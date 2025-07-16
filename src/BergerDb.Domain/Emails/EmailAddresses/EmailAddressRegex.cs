using System.Text.RegularExpressions;

namespace BergerDb.Domain.Emails.EmailAddresses;

internal static partial class EmailAddressRegex
{
    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled)]
    internal static partial Regex GetRegex();
}
