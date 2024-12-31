using BergerDb.Shared.Entities;

namespace BergerDb.Domain.Customers;

public record CustomerId(Guid Value) : EntityId(Value);
