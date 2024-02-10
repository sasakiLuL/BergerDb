using BergerDb.Application.Core.Abstractions.Authorization;
using Microsoft.AspNetCore.Http;

namespace BergerDb.Infrastructure.Authentication;

public class UserIdentifierProvider : IUserIdetifierProvider
{
    public UserIdentifierProvider(IHttpContextAccessor context)
    {
        string userIdClaim = context.HttpContext?.User?.Claims.FirstOrDefault()?.Value ??
            throw new ArgumentException("The user identifier claim is required.", nameof(context));

        UserId = Guid.Parse(userIdClaim);
    }

    public Guid UserId { get; }
}
