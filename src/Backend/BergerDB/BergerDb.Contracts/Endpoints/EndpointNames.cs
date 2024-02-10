namespace BergerDb.Api.Contracts;

public static class EndpointNames
{
    public static class Authentication
    {
        public const string Login = "Login";

        public const string Register = "Register";
    }

    public static class Users
    {
        public const string Get = "GetUsers";

        public const string GetById = "GetUserById";

        public const string Update = "UpdateUser";

        public const string ChangePassword = "ChangeUserPassword";

        public const string Delete = "DeleteUser";

        public const string GetEmailConfiguration = "GetEmailConfiguration";

        public const string UpdateEmailConfiguration = "UpdateEmailConfiguration";
    }

    public static class Customers
    {
        public const string Create = "CreateCustomer";

        public const string Get = "GetCustomers";

        public const string GetReminders = "GetReminders";

        public const string GetById = "GetCustomerById";

        public const string UpdateAddress = "UpdateAddress";

        public const string UpdateCustomer = "UpdateCustomer";

        public const string UpdateMembership = "UpdateMembership";

        public const string Delete = "DeleteCustomer";

        public const string SendInvoice = "SendInvoice";

        public const string SendDunning = "SendDunning";

        public const string UpdateNotation = "UpdateNotation";
    }
}