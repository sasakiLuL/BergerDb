using BergerDb.Application.Core.Abstractions.Messaging;

namespace BergerDb.Application.Users.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string UserName,
    string? FirstName,
    string? LastName) : ICommand;