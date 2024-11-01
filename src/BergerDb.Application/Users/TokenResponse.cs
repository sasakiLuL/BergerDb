namespace BergerDb.Application.Users;

public record TokenResponse(
    string Type,
    string Token,
    long ExpiresIn);
