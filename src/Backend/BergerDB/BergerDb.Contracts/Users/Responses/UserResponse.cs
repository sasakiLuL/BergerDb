using BergerDb.Contracts.Common;

namespace BergerDb.Contracts.Users.Responses;

public record UserResponse(
    Guid Id,
    string UserName,
    string Email,
    string? FirstName,
    string? LastName)
{
    public List<Link> Links { get; } = [];
}
