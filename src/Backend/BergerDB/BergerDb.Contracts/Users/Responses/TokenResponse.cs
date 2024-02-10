using BergerDb.Contracts.Common;

namespace BergerDb.Contracts.Users.Responses;

public record TokenResponse(
    string Id,
    string Token)
{
    public List<Link> Links { get; } = [];
}
