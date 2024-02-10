namespace BergerDb.Contracts.Users.Requests;

public record UpdateUserRequest(
    string UserName,
    string? FirstName,
    string? LastName);
