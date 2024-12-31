using BergerDb.Domain.Customers;
using BergerDb.Domain.Emails;
using BergerDb.Domain.PaymentProcesses;
using BergerDb.Domain.Payments;
using BergerDb.Domain.PdfTemplates;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BergerDb.Persistanse;

public class BergerDbContext : DbContext
{
    public BergerDbContext()
    {
    }

    public BergerDbContext(DbContextOptions<BergerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=berger.db;");

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Customer> Customers { get; private set; }

    public DbSet<PaymentProcess> PaymentProcesses { get; private set; }

    public DbSet<Payment> Payments { get; private set; }

    public DbSet<Email> Emails { get; private set; }

    public DbSet<PdfTemplate> PdfTemplates { get; private set; }
}
