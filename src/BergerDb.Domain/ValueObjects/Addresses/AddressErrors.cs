using BergerDb.Shared.Errors;

namespace BergerDb.Domain.ValueObjects.Addresses;

public static class AddressErrors
{
    public static readonly Error TooLong = new(
        "Address.TooLong",
        "The address is too long.");

    public static readonly Error InvalidFormat = new(
        "Address.InvalidFormat",
        "The address should not contain special symbols.");
}
