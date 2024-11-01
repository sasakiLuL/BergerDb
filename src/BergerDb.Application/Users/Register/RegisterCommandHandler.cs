using BergerDb.Application.Abstractions.Authorization;
using BergerDb.Application.Abstractions.Messaging;
using BergerDb.Core.Results;
using BergerDb.Domain.Abstractions.PasswordHashing;
using BergerDb.Domain.Abstractions.Repositories;
using BergerDb.Domain.Profiles.ProfileIds;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.Emails;
using BergerDb.Domain.Users.Passwords;
using BergerDb.Domain.Users.UserIds;

namespace BergerDb.Application.Users.Register;

public class RegisterCommandHandler(
    IPasswordHasher passwordHasher,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenResponseProvider tokenResponseProvider) : ICommandHandler<RegisterCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);

        var passwordResult = Password.Create(request.Password);

        var validationResult = Result.AllFailuresOrSuccess(emailResult, passwordResult);

        if (validationResult.IsFailure)
        {
            return Result.Failure<TokenResponse>(validationResult.Errors);
        }

        if (!await userRepository.IsEmailUniqueAsync(emailResult.Value))
        {
            return Result.Failure<TokenResponse>(UserErrors.DuplicateEmail);
        }

        var user = new User(
            new UserModel()
            {
                Id = new UserId(Guid.NewGuid()),
                Email = emailResult.Value,
                PasswordHash = passwordHasher.Hash(passwordResult.Value.Value),
                ProfileId = new ProfileId(Guid.NewGuid()),
            });

        await userRepository.AddAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var tokenResponse = tokenResponseProvider.Create(user);

        return Result.Success(tokenResponse);
    }
}
