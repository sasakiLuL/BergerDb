using BergerDb.Domain.Users;

namespace BergerDb.Application.Core.Abstractions.Authorization;

public interface IJwtProvider
{
    Task<string> CreateAsync(User user);
}
