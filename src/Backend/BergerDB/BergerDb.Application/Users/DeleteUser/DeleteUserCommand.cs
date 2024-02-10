using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Users.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand;
