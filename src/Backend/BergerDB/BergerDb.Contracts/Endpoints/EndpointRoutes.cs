namespace BergerDb.Api.Contracts;

public static class EndpointRoutes
{
    public static string Api = "api";

    public static class Authentication
    {
        public const string Login = "authentication/login";

        public const string Register = "authentication/register";
    }

    public static class Users
    {
        public const string Get = "users/";

        public const string GetById = "users/{id:guid}";

        public const string Update = "users/{id:guid}";

        public const string ChangePassword = "users/{id:guid}/change-password";

        public const string Delete = "users/{id:guid}";

        public const string GetEmailConfiguration = "users/{id:guid}/email-config";

        public const string UpdateEmailConfiguration = "users/{id:guid}/email-config";
    }

    public static class Customers
    {
        public const string Create = "customers/";

        public const string Get = "customers/";

        public const string GetById = "customers/{id:guid}";

        public const string UpdateAddress = "customers/{id:guid}/address";

        public const string UpdateCustomer = "customers/{id:guid}";

        public const string UpdateMembership = "customers/{id:guid}/membership";

        public const string Delete = "customers/{id:guid}";

        public const string SendInvoice = "customers/{id:guid}/send-invoice";

        public const string SendDunning = "customers/{id:guid}/send-dunning";

        public const string UpdateNotation = "customers/{id:guid}/notation";
    }
}
