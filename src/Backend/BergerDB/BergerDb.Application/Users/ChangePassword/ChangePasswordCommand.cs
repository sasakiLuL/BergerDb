using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Users.ChangePassword;

public record ChangePasswordCommand(
    Guid UserId,
    string Password) : ICommand;
