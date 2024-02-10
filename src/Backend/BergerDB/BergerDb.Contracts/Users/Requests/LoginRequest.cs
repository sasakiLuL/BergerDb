namespace BergerDb.Contracts.Users.Requests;

public record LoginRequest(
    string Email,
    string Password);
