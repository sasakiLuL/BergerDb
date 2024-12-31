using BergerDb.Shared.Errors;

namespace BergerDb.Domain.Customers.Notations;

public static class NotationErrors
{
    public static Error TooLong = new(
        "Notation.TooLong", 
        "The notation is too long");
}
