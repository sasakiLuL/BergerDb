namespace BergerDb.Contracts.Customers.Requests;

public record UpdateCustomerPersonalInfoRequest(
    string Prefix,
    string FirstName,
    string LastName,
    string Email,
    long PersonalId,
    int Sex,
    DateTime RegistrationDate);
