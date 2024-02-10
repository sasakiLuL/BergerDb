using BergerDb.Application.Core.Abstractions.Authorization;
using BergerDb.Application.Core.Abstractions.Cryptography;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.Passwords;

namespace BergerDb.Application.Users.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;

    private readonly IUserIdetifierProvider _userIdetifierProvider;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordCommandHandler(IUserRepository userRepository, IUserIdetifierProvider userIdetifierProvider, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _userIdetifierProvider = userIdetifierProvider;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken = default)
    {
        if (request.UserId != _userIdetifierProvider.UserId)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        var passwordResult = await Password.CreateAsync(request.Password);

        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Errors);
        }

        User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        string passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

        Result result = user.ChangePassword(passwordHash);

        if (result.IsFailure) 
        {
            return Result.Failure(result.Errors);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
