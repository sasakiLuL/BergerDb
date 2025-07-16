using BergerDb.Domain.PdfTemplates;

namespace BergerDb.Persistanse.PdfTemplates;

public class PdfTemplateRepository : Repository<PdfTemplate>, IPdfTemplateRepository
{
    public PdfTemplateRepository(BergerDbContext context) : base(context)
    {
    }
}
