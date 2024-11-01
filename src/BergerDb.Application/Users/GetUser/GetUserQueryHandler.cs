using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Core.Results;

namespace BergerDb.Application.Users.GetUser;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, List<UserResponse>>
{
    public Task<Result<List<UserResponse>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
