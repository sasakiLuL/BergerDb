using BergerDb.Domain.Abstractions;

namespace BergerDb.Domain.Emails;

public interface IEmailRepository : IRepository<Email, EmailId>
{
}
