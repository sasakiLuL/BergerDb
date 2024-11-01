using BergerDb.Core.Entities;
using BergerDb.Domain.Profiles.ProfileIds;
using BergerDb.Domain.Users.Emails;
using BergerDb.Domain.Users.UserIds;

namespace BergerDb.Domain.Users;

public class UserModel : IModel
{
    public required UserId Id { get; init; }

    public required Email Email { get; set; }

    public required string PasswordHash { get; set; }

    public required ProfileId ProfileId { get; set; }
}
