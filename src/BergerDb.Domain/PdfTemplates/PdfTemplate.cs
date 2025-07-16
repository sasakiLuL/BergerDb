using BergerDb.Shared.Entities;

namespace BergerDb.Domain.PdfTemplates;

public class PdfTemplate : Entity
{
#pragma warning disable CS8618
    private PdfTemplate() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618

    public PdfTemplate(
        Guid id, 
        string color) : base(id)
    {
        Color = color;
    }

    public string Color { get; set; }
}
