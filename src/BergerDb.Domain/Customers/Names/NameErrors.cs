using BergerDb.Shared.Errors;

namespace BergerDb.Domain.Customers.Names;

public static class NameErrors
{
    public static readonly Error TooLong = new(
        "Name.TooLong",
        "The firstname is too short.");

    public static readonly Error InvalidFormat = new(
        "Name.InvalidFormat",
        "The name should not contain any special symbols.");
}
