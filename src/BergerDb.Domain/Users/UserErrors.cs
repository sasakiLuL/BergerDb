using BergerDb.Core.Errors;

namespace BergerDb.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound", "The user with the specified identifier was not found.");

    public static readonly Error DuplicateEmail = new(
        "User.DuplicateEmail", "The specified email is already in use.");

    public static readonly Error CannotChangePassword = new(
        "User.CannotChangePassword", "The password cannot be changed to the specified password.");

    public static readonly Error InvalidPermissions = new(
        "User.InvalidPermissions", "The current user does not have the permissions to perform that operation.");

    public static class Authorization
    {
        public static readonly Error InvalidEmail = new(
            "User.InvalidEmail", "The user with specified email was not found.");

        public static readonly Error InvalidPassword = new(
            "User.InvalidPassword", "The password is wrong.");
    }
}
