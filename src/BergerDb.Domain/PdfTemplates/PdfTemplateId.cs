using BergerDb.Shared.Entities;

namespace BergerDb.Domain.PdfTemplates;

public record PdfTemplateId(Guid Value) : EntityId(Value);
