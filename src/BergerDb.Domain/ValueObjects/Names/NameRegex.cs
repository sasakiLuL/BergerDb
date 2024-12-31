using System.Text.RegularExpressions;

namespace BergerDb.Domain.ValueObjects.Names;

internal static partial class NameRegex
{
    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled)]
    internal static partial Regex GetRegex();
}
