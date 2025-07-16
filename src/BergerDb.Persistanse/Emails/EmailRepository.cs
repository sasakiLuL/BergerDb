using BergerDb.Domain.Emails;

namespace BergerDb.Persistanse.Emails;

public class EmailRepository : Repository<Email>, IEmailRepository
{
    public EmailRepository(BergerDbContext context) : base(context)
    {
    }
}
