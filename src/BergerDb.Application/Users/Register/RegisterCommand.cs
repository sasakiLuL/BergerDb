using BergerDb.Application.Abstractions.Messaging;

namespace BergerDb.Application.Users.Register;

public record RegisterCommand(
    string Email, 
    string Password) : ICommand<TokenResponse>;
