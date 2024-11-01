using BergerDb.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BergerDb.Persistance;

public class BergerDbContext : DbContext
{
    public BergerDbContext(DbContextOptions<BergerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserModel> Users { get; private set; }
}
