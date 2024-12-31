using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Payments;

public record PaymentId(Guid Value) : EntityId(Value);
