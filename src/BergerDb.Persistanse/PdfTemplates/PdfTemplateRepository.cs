using BergerDb.Domain.PdfTemplates;

namespace BergerDb.Persistanse.PdfTemplates;

public class PdfTemplateRepository : Repository<PdfTemplate, PdfTemplateId>, IPdfTemplateRepository
{
    public PdfTemplateRepository(BergerDbContext context) : base(context)
    {
    }
}
