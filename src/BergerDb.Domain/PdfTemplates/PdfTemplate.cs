using BergerDb.Shared.Entities;

namespace BergerDb.Domain.PdfTemplates;

public class PdfTemplate : Entity<PdfTemplateId>
{
#pragma warning disable CS8618
    private PdfTemplate() : base(new PdfTemplateId(Guid.NewGuid())) { }
#pragma warning restore CS8618

    public PdfTemplate(
        PdfTemplateId id, 
        string color) : base(id)
    {
        Color = color;
    }

    public string Color { get; set; }
}
