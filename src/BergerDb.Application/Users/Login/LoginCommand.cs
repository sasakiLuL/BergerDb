using BergerDb.Application.Abstractions.Messaging;

namespace BergerDb.Application.Users.Login;

public record LoginCommand(
    string Email, 
    string Password) : ICommand<TokenResponse>;
