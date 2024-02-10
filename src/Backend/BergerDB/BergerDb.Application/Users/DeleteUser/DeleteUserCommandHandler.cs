using BergerDb.Application.Core.Abstractions.Authorization;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.EmailConfigurations;

namespace BergerDb.Application.Users.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserIdetifierProvider _userIdetifierProvider;

    private readonly IUserRepository _userRepository;

    private readonly IEmailConfigurationRepository _emailConfigurationRepository;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IUserIdetifierProvider userIdetifierProvider, IUserRepository userRepository, IEmailConfigurationRepository emailConfigurationRepository)
    {
        _unitOfWork = unitOfWork;
        _userIdetifierProvider = userIdetifierProvider;
        _userRepository = userRepository;
        _emailConfigurationRepository = emailConfigurationRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != _userIdetifierProvider.UserId) 
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        _emailConfigurationRepository.Delete(user.EmailConfiguration!);

        _userRepository.Delete(user);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
