using BergerDb.Shared.Errors;

namespace BergerDb.Domain.Customers.Prefixes;

public static class PrefixErrors
{
    public static Error TooLong = new(
        "Prefix.TooLong", 
        "The name title is too long");

    public static Error InvalidFormat = new(
        "Prefix.InvalidFormat", 
        "The name title format is invalid.");
}
