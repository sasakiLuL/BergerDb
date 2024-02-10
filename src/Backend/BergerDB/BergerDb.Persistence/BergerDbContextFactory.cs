using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BergerDb.Persistence;

public class BergerDbContextFactory :
    IDesignTimeDbContextFactory<BergerDbContext>
{
    public BergerDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<BergerDbContext>();
        builder.UseNpgsql(@"User ID=postgres;Password=pass12345;Host=localhost;Port=5433;Database=BergerDb;");
        return new BergerDbContext(builder.Options);
    }
}
