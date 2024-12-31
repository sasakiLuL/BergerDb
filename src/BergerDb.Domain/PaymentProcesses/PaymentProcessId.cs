using BergerDb.Shared.Entities;

namespace BergerDb.Domain.PaymentProcesses;

public record PaymentProcessId(Guid Value) : EntityId(Value);
