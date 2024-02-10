using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Common;
using BergerDb.Contracts.Users.Responses;

namespace BergerDb.Application.Users.GetUsers;

public record GetUsersQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<UserResponse>>;
