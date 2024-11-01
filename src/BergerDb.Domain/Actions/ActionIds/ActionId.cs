using BergerDb.Core.Entities;

namespace BergerDb.Domain.Actions.ActionIds;

public record ActionId(Guid Value) : IEntityId;
