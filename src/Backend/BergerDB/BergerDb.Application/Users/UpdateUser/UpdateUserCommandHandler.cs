using BergerDb.Application.Core.Abstractions;
using BergerDb.Application.Core.Abstractions.Authorization;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.UserNames;

namespace BergerDb.Application.Users.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserIdetifierProvider _userIdetifierProvider;

    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserIdetifierProvider userIdetifierProvider, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userIdetifierProvider = userIdetifierProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken = default)
    {
        if (request.UserId != _userIdetifierProvider.UserId) 
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        var userNameResult = await UserName.CreateAsync(request.UserName);

        var firstNameResult = await FirstName.CreateAsync(request.FirstName);

        var lastNameResult = await LastName.CreateAsync(request.LastName);

        Result firstFailureOrSucces = Result.Concat(
            firstNameResult, lastNameResult);

        if (firstFailureOrSucces.IsFailure)
        {
            return Result.Failure(firstNameResult.Errors);
        }

        User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        user.ChangeUserName(userNameResult.Value);

        user.ChangeName(firstNameResult.Value, lastNameResult.Value);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
