using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Emails;

public record EmailId(Guid Value) : EntityId(Value);
