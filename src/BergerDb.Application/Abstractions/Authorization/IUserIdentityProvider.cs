namespace BergerDb.Application.Abstractions.Authorization;

public interface IUserIdentityProvider
{
    string UserId { get; }
}
