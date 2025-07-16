using BergerDb.Shared.Errors;

namespace BergerDb.Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound => new(
        "Customer.NotFound",
        "The customer with specified id was not found.");

    public static Error InvalidFieldName => new(
        "Customer.InvalidFieldName",
        "The field name is invalid.");
}
