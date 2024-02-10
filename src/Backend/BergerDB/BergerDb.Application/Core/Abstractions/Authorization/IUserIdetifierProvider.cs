namespace BergerDb.Application.Core.Abstractions.Authorization;

public interface IUserIdetifierProvider
{
    Guid UserId { get; }
}
