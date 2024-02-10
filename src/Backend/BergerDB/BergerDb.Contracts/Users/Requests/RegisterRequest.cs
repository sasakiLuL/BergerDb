namespace BergerDb.Contracts.Users.Requests;

public record RegisterRequest(
    string UserName,
    string Email,
    string Password,
    string? FirstName,
    string? LastName,
    IEnumerable<long> Roles);
