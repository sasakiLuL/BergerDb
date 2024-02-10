using BergerDb.Application.Core.Abstractions.Authorization;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Users.Responses;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Abstractions.Services;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.Passwords;

namespace BergerDb.Application.Users.Login;

public sealed class LoginCommandHandler
    : ICommandHandler<LoginCommand, TokenResponse>
{
    private readonly IUserRepository _userRepository;

    private readonly IPasswordHashChecker _passwordHashChecker;

    private readonly ITokenResponseLinksService _tokenResponseLinksService;

    private readonly IJwtProvider _jwtProvider;

    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHashChecker passwordHashChecker,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork,
        ITokenResponseLinksService tokenResponseLinksService)
    {
        _userRepository = userRepository;
        _passwordHashChecker = passwordHashChecker;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
        _tokenResponseLinksService = tokenResponseLinksService;
    }

    public async Task<Result<TokenResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        Result<Email> emailResult = await Email.CreateAsync(request.Email);

        Result<Password> passwordResult = await Password.CreateAsync(request.Password);

        Result firstFailureOrSucces = Result.Concat(
            emailResult, passwordResult);

        if (firstFailureOrSucces.IsFailure)
        {
            return Result.Failure<TokenResponse>(firstFailureOrSucces.Errors);
        }

        User? user = await _userRepository.GetUserByEmailAsync(emailResult.Value);

        if (user is null)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmail);
        }

        if (!user.VerifyPasswordHash(passwordResult.Value.Value, _passwordHashChecker))
        {
            return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidPassword);
        }

        await _unitOfWork.SaveChangesAsync();

        var token = new TokenResponse(user.Id.ToString(), await _jwtProvider.CreateAsync(user));

        _tokenResponseLinksService.GenerateLinks(token);

        return Result.Success(token);
    }
}
