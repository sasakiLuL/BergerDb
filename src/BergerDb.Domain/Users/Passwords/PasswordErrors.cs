using BergerDb.Core.Errors;

namespace BergerDb.Domain.Users.Passwords;

public static class PasswordErrors
{
    public static readonly Error TooShort = new(
        "Password.TooShort", "The password is too short.");

    public static readonly Error TooLong = new(
        "Password.TooLong", "The password is too long.");

    public static readonly Error MissingDigit = new(
        "Password.MissingDigit", "The password requires at least one digit.");

    public static readonly Error MissingLetter = new(
        "Password.MissingLetter", "The password requires at least one letter.");

    public static readonly Error InvalidFormat = new(
        "Password.InvalidFormat", "The password format is invalid.");
}
