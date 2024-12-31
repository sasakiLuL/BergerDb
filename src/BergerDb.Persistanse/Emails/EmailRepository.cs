using BergerDb.Domain.Emails;

namespace BergerDb.Persistanse.Emails;

public class EmailRepository : Repository<Email, EmailId>, IEmailRepository
{
    public EmailRepository(BergerDbContext context) : base(context)
    {
    }
}
