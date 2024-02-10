using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Customers;
using BergerDb.Contracts.Users.Responses;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Users;
using Mapster;

namespace BergerDb.Application.Users.GetUser;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;

    private readonly IUserResponseLinksService _userResponseLinksService;

    public GetUserQueryHandler(IUserRepository userRepository, IUserResponseLinksService userResponseLinksService)
    {
        _userRepository = userRepository;
        _userResponseLinksService = userResponseLinksService;
    }

    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(DomainErrors.User.NotFound);
        }

        var response = user.Adapt<UserResponse>();

        _userResponseLinksService.GenerateLinks(response);

        return Result.Success(response);
    }
}
