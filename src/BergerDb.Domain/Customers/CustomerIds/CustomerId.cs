using BergerDb.Core.Entities;

namespace BergerDb.Domain.Customers.CustomerIds;

public record CustomerId(Guid Value) : IEntityId;
