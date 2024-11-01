using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Core.Results;
using BergerDb.Domain.Users.Emails;
using BergerDb.Domain.Users.Passwords;
using BergerDb.Domain.Users;
using BergerDb.Domain.Abstractions.PasswordHashing;
using BergerDb.Application.Abstractions.Authorization;

namespace BergerDb.Application.Users.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenResponseProvider tokenResponseProvider) : ICommandHandler<LoginCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);

        var passwordResult = Password.Create(request.Password);

        var validationResult = Result.AllFailuresOrSuccess(emailResult, passwordResult);

        if (validationResult.IsFailure)
        {
            return Result.Failure<TokenResponse>(validationResult.Errors);
        }

        var user = await userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

        if (user is null)
        {
            return Result.Failure<TokenResponse>(UserErrors.Authorization.InvalidEmail);
        }

        if (!passwordHasher.Verify(user.Model.PasswordHash, passwordResult.Value.Value))
        {
            return Result.Failure<TokenResponse>(UserErrors.Authorization.InvalidPassword);
        }

        var tokenResponse = tokenResponseProvider.Create(user);

        return Result.Success(tokenResponse);
    }
}
