using BergerDb.Application.Users;
using BergerDb.Domain.Users;

namespace BergerDb.Application.Abstractions.Authorization;

public interface ITokenResponseProvider
{
    TokenResponse Create(User user);
}
