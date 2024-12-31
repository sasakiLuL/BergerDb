using BergerDb.Shared.Errors;

namespace BergerDb.Domain.Customers.ZipCodes;

public static class ZipCodeErrors
{
    public static readonly Error TooLong = new(
        "ZipCode.InvalidLenght",
        "The postalcode is to long");

    public static readonly Error InvalidFormat = new(
        "ZipCode.InvalidFormat",
        "The postalcode format is invalid.");
}
