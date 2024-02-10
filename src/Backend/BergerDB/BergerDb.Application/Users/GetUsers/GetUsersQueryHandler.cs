using BergerDb.Application.Core.Abstractions;
using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Common;
using BergerDb.Contracts.Users.Responses;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Users;
using Mapster;
using System.Linq.Expressions;

namespace BergerDb.Application.Users.GetUsers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagedList<UserResponse>>
{
    private readonly IDbContextService _dbContextService;

    private readonly IUserResponseLinksService _userResponseLinksService;

    public GetUsersQueryHandler(IDbContextService dbContextService, IUserResponseLinksService userResponseLinksService)
    {
        _dbContextService = dbContextService;
        _userResponseLinksService = userResponseLinksService;
    }
    public async Task<Result<PagedList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var userQuery = _dbContextService.GetQuaryable<User>();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            userQuery = userQuery.Where(c =>
                c.UserName.Value.Contains(request.SearchTerm));
        }

        var sortColumn = GetSortColumn(request);

        if (request.SortOrder?.ToLower() == "desc")
        {
            userQuery = userQuery.OrderByDescending(sortColumn);
        }
        else
        {
            userQuery = userQuery.OrderBy(sortColumn);
        }

        var userResponsesQuery = userQuery
            .Select(u => u.Adapt<UserResponse>());

        var users = await PagedList<UserResponse>.CreateAsync(
            userResponsesQuery,
            request.Page,
            request.PageSize,
            cancellationToken);

        users.Items.ForEach((item) => _userResponseLinksService.GenerateLinks(item));

        return Result.Success(users);
    }

    private Expression<Func<User, object>> GetSortColumn(GetUsersQuery request) =>
        request.SortColumn?.ToLower() switch
        {
            "username" => user => user.UserName,
            "email" => user => user.Email,
            "firstname" => user => user.FirstName!,
            "lastname" => user => user.LastName!,
            _ => user => user.Id
        };
}