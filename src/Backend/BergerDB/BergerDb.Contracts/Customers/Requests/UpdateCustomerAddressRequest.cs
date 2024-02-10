namespace BergerDb.Contracts.Customers.Requests;

public record UpdateCustomerAddressRequest(
    string Street,
    string ZipCode,
    string City);
