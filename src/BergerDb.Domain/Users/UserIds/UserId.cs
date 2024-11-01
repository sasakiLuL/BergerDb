using BergerDb.Core.Entities;

namespace BergerDb.Domain.Users.UserIds;

public record UserId(Guid Value) : IEntityId;
