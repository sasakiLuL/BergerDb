using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Users.Responses;

namespace BergerDb.Application.Users.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password,
    string? FirstName,
    string? LastName,
    IEnumerable<long> Roles) : ICommand<TokenResponse>;
