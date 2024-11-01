namespace BergerDb.Application.Users;

public record UserResponse(
    string Email, 
    Guid ProfileId);
