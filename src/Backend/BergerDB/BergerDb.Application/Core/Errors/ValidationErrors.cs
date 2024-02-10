using BergerDb.Domain.Core.Primitives;

namespace BergerDb.Application.Core.Errors;

internal static class ValidationErrors
{
    internal static class Login
    {
        internal static Error EmailIsRequired =>
            new Error("Login.EmailIsRequired", "The email is required.");

        internal static Error PasswordIsRequired =>
            new Error("Login.PasswordIsRequired", "The password is required.");
    }

    internal static class User
    {
        internal static Error UserNameIsRequired =>
           new ("Registration.UserNameIsRequired", "The username is required.");

        internal static Error EmailIsRequired =>
            new ("Registration.EmailIsRequired", "The email is required.");

        internal static Error PasswordIsRequired =>
            new ("Registration.PasswordIsRequired", "The password is required.");

        internal static Error RoleIsRequired =>
            new ("Registration.RoleIsRequired", "The role is required.");

        internal static Error DuplicateRoles =>
            new ("Registration.DuplicateRoles", "The duplications in role list");
    }

    internal static class Address
    {
        internal static Error StreetIsRequired =>
            new ("Address.StreetIsRequired", "The street is required.");

        internal static Error ZipCodeIsRequired =>
            new ("Address.ZipCodeIsRequired", "The zip code is required.");

        internal static Error CityIsRequired =>
            new ("Address.CityIsRequired", "The city is required.");
    }

    internal static class Membership
    {
        internal static Error WrongMemberTypeValue =>
            new ("Membership.WrongMemberTypeValue", "The member type value is wrong.");

        internal static Error WrongEntryTypeValue =>
            new ("Membership.WrongEntryTypeValue", "The entry type value is wrong.");

        internal static Error WrongPaymentMethodValue =>
            new ("Membership.WrongPaymentMethodValue", "The payment method is wrong.");

        internal static Error AmountIsRequired =>
            new ("Membership.AmountIsRequired", "The money value is required.");

        internal static Error WrongMoneyValue =>
            new ("Membership.WrongMoneyValue", "The money value is wrong.");

        internal static Error MailedIsRequired =>
            new ("Membership.MailedIsRequired", "The mailed value is required");
    }

    internal static class Customer
    {
        internal static Error FirstNameIsRequired =>
           new Error("Customer.FirstNameIsRequired", "The firstname is required.");

        internal static Error LastNameIsRequired =>
           new Error("Customer.LastNameIsRequired", "The lastname is required.");

        internal static Error EmailIsRequired =>
            new Error("Customer.EmailIsRequired", "The email is required.");

        internal static Error SexIsRequired =>
            new Error("Customer.SexIsRequired", "The sex value is required.");

        internal static Error WrongSexValue =>
            new Error("Customer.WrongSexValue", "The sex value is wrong.");
    }
}
