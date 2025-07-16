using System.Text.RegularExpressions;

namespace BergerDb.Domain.Customers.Names;

internal static partial class NameRegex
{
    [GeneratedRegex(@"^[\p{L}0-9 ,\.\/\\!@#$%&*+()_-]*$", RegexOptions.Compiled)]
    internal static partial Regex GetRegex();
}
