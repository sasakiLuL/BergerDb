using BergerDb.Domain.Core.Primitives;

namespace BergerDb.Domain.Core.Errors;

public static class DomainErrors
{
    public static class Authentication
    {
        public static Error InvalidEmail => new(
            "Authentication.InvalidEmail",
            "The specified email is incorrect.");

        public static Error InvalidPassword => new(
            "Authentication.InvalidPassword",
            "The specified password is incorrect.");
    }

    public static class User
    {
        public static Error NotFound =>
            new("User.NotFound", "The user with the specified identifier was not found.");

        public static Error DuplicateEmail
            => new("User.DuplicateEmail", "The specified email is already in use.");

        public static Error DuplicateUserName
            => new("User.DuplicateUserName", "The specified username is already in use.");

        public static Error CannotChangePassword
            => new("User.CannotChangePassword", "The password cannot be changed to the specified password.");

        public static Error InvalidPermissions =>
            new("User.InvalidPermissions", "The current user does not have the permissions to perform that operation.");
    }

    public static class Role
    {
        public static Error NotFound =>
            new("Role.NotFound", "The role with the specified identifier was not found.");
    }

    public static class Customer
    {
        public static Error NotFound =>
            new("Customer.NotFound", "The customer with the specified identifier was not found.");
    }

    public static class UserName
    {
        public static readonly Error TooShort =
            new("UserName.TooShort", "The username is too short.");

        public static readonly Error TooLong =
            new("UserName.TooLong", "The username is too long.");

        public static readonly Error InvalidFormat =
            new("UserName.InvalidFormat", "The username format is invalid.");
    }

    public static class Email
    {
        public static readonly Error TooLong =
            new("Email.TooLong", "The email is too long.");

        public static readonly Error InvalidFormat =
            new("Email.InvalidFormat", "The email format is invalid.");
    }

    public static class Password
    {
        public static readonly Error TooShort =
            new("Password.TooShort", "The password is too short.");

        public static readonly Error TooLong =
            new("Password.TooLong", "The password is too long.");

        public static readonly Error MissingDigit =
            new("Password.MissingDigit", "The password requires at least one digit.");

        public static readonly Error MissingLetter =
            new("Password.MissingLetter", "The password requires at least one letter.");

        public static readonly Error InvalidFormat =
            new("Password.InvalidFormat", "The password format is invalid.");
    }

    public static class FirstName
    {
        public static readonly Error TooLong =
            new("FirstName.TooLong", "The firstname is too short.");

        public static readonly Error InvalidFormat =
            new("FirstName.InvalidFormat", "The firstname can contain only letters.");
    }

    public static class LastName
    {
        public static readonly Error TooLong =
            new("LastName.TooLong", "The lastname is too short.");

        public static readonly Error InvalidFormat =
            new("LastName.InvalidFormat", "The lastname can contain only letters.");
    }

    public static class Institution
    {
        public static readonly Error TooLong =
            new("Institution.TooLong", "The institution name is too long.");

        public static readonly Error InvalidFormat =
            new("Institution.InvalidFormat", "The institution name can not contain special symbols.");
    }

    public static class AddressName
    {
        public static readonly Error TooLong =
            new("AddressName.TooLong", "The address name is too long.");

        public static readonly Error InvalidFormat =
            new("AddressName.InvalidFormat", "The address name can not contain special symbols.");
    }

    public static class ZipCode
    {
        public static readonly Error TooLong =
            new("ZipCode.InvalidLenght", "The postalcode is to long");

        public static readonly Error InvalidFormat =
            new("ZipCode.InvalidFormat", "The postalcode format is invalid.");
    }

    public static class General
    {
        public static Error UnProcessableRequest => new Error(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
    }

    public static class InvoiceDateRange
    {
        public static Error ValueError => new Error(
            "InvoiceDateRange.ValueError",
            "The curent invoice date should be greater than last.");
    }

    public static class Prefix
    {
        public static Error TooLong =
            new("Prefix.TooLong", "The name title is too long");

        public static Error InvalidFormat =
            new("Prefix.InvalidFormat", "The name title format is invalid.");
    }

    public static class Notation
    {
        public static Error TooLong =
            new("Notation.TooLong", "The notation is too long");
    }
}
