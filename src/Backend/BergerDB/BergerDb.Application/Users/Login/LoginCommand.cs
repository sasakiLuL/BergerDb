using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Users.Responses;

namespace BergerDb.Application.Users.Login;

public record LoginCommand(
    string Email,
    string Password) : ICommand<TokenResponse>;
