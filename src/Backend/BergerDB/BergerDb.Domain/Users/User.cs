using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Abstractions.Services;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Shared.Email;
using BergerDb.Domain.Shared.FirstNames;
using BergerDb.Domain.Shared.LastNames;
using BergerDb.Domain.Users.EmailConfigurations;
using BergerDb.Domain.Users.Roles;
using BergerDb.Domain.Users.UserNames;

namespace BergerDb.Domain.Users;

public sealed class User : Entity, IAuditableEntity, ISoftDeletableEntity
{
    private string _passwordHash;

    private User() : base(Guid.NewGuid()) {}

    public User(
        UserName username,
        Email email,
        string passwordHash,
        FirstName? firstName,
        LastName? lastName,
        EmailConfiguration? emailConfiguration,
        ICollection<Role> roles) : base(Guid.NewGuid())
    {
        UserName = username;
        Email = email;
        _passwordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        Roles = roles;
        EmailConfiguration = emailConfiguration;
    }

    public UserName UserName { get; private set; }

    public Email Email { get; }

    public FirstName? FirstName { get; private set; }

    public LastName? LastName { get; private set; }

    public DateTime CreatedOnUtc { get; }

    public DateTime? LastModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public ICollection<Role> Roles { get; }

    public EmailConfiguration? EmailConfiguration { get; private set; }

    public bool VerifyPasswordHash(string password, IPasswordHashChecker checker)
        => !string.IsNullOrWhiteSpace(password) && checker.HashesMatch(_passwordHash, password);

    public Result ChangePassword(string passwordHash)
    {
        if (passwordHash == _passwordHash)
        {
            return Result.Failure(DomainErrors.User.CannotChangePassword);
        }

        _passwordHash = passwordHash;

        return Result.Success();
    }

    public void ChangeUserName(UserName userName)
    {
        UserName = userName;
    }

    public void ChangeName(FirstName? firstName, LastName? lastName)
    {
        FirstName = firstName;

        LastName = lastName;
    }
}
