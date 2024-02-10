using BergerDb.Application.Core.Abstractions.Authorization;
using BergerDb.Application.Core.Abstractions.Cryptography;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Users.Responses;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.EmailConfigurations;
using BergerDb.Domain.Users.Passwords;
using BergerDb.Domain.Users.Roles;
using BergerDb.Domain.Users.UserNames;

namespace BergerDb.Application.Users.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, TokenResponse>
{
    private readonly IUserRepository _userRepository;

    private readonly IRoleRepository _roleRepository;

    private readonly IEmailConfigurationRepository _emailConfigurationRepository;

    private readonly ITokenResponseLinksService _tokenResponseLinksService;

    private readonly IJwtProvider _jwtProvider;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IRoleRepository roleRepository,
        IEmailConfigurationRepository emailConfigurationRepository,
        ITokenResponseLinksService tokenResponseLinksService)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
        _emailConfigurationRepository = emailConfigurationRepository;
        _tokenResponseLinksService = tokenResponseLinksService;
    }

    public async Task<Result<TokenResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userNameResult = await UserName.CreateAsync(request.UserName);

        var emailResult = await Email.CreateAsync(request.Email);

        var passwordResult = await Password.CreateAsync(request.Password);

        var firstNameResult = await FirstName.CreateAsync(request.FirstName);

        var lastNameResult = await LastName.CreateAsync(request.LastName);

        var firstFailureOrSucces = Result.Concat(
            userNameResult,
            emailResult,
            passwordResult,
            firstNameResult,
            lastNameResult);

        if (firstFailureOrSucces.IsFailure)
        {
            return Result.Failure<TokenResponse>(firstFailureOrSucces.Errors);
        }

        if (!await _userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<TokenResponse>(DomainErrors.User.DuplicateEmail);
        }

        if (!await _userRepository.IsUserNameUniqueAsync(userNameResult.Value, cancellationToken))
        {
            return Result.Failure<TokenResponse>(DomainErrors.User.DuplicateUserName);
        }

        var roles = await _roleRepository.GetRolesByIdAsync([.. request.Roles], cancellationToken);

        if (roles.Count() == 0)
        {
            return Result.Failure<TokenResponse>(DomainErrors.Role.NotFound);
        }

        var passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

        User user = new User(
            userNameResult.Value,
            emailResult.Value,
            passwordHash,
            firstNameResult.Value,
            lastNameResult.Value,
            null,
            [.. roles]);

        _userRepository.Add(user);

        EmailConfiguration emailConfiguration = new EmailConfiguration(
            "Strasse...",
            "Stadt...",
            "12345",
            "+123456789",
            "example@mail.com",
            "www.homapage.com",
            "Konto...",
            "IBAN...",
            "BIC...",
            "GID...",
            "Steuernummer...",
            """{"blocks":[{"key":"cic53","text":"","type":"unstyled","depth":0,"inlineStyleRanges":[],"entityRanges":[],"data":{}}],"entityMap":{}}""",
            "Rechnung",
            "Rechnung korper",
            "Erinnerung",
            "Erinnerung korper",
            "Mahnung",
            "Mahnung korper",
            user.Id,
            user);

        _emailConfigurationRepository.Add(emailConfiguration);

        await _unitOfWork.SaveChangesAsync();

        var token = new TokenResponse(user.Id.ToString(), await _jwtProvider.CreateAsync(user));

        _tokenResponseLinksService.GenerateLinks(token);

        return Result.Success(token);
    }
}
