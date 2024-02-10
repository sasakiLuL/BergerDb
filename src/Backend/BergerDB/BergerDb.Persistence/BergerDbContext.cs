using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Addresses;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Users;
using BergerDb.Domain.Users.EmailConfigurations;
using BergerDb.Domain.Users.Permissions;
using BergerDb.Domain.Users.Roles;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BergerDb.Persistence;

public class BergerDbContext : DbContext
{
    public BergerDbContext(DbContextOptions<BergerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Membership> Memberships { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<EmailConfiguration> EmailConfigurations { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
