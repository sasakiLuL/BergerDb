using BergerDb.Core.Entities;

namespace BergerDb.Domain.Profiles.ProfileIds;

public record ProfileId(Guid Value) : IEntityId;
