using BergerDb.Application.Abstractions.Messaging;

namespace BergerDb.Application.Users.GetUser;

public record GetUserQuery(Guid Id) : IQuery<List<UserResponse>>;
