using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Users.Responses;

namespace BergerDb.Application.Users.GetUser;

public record GetUserQuery(
    Guid Id) : IQuery<UserResponse>;
