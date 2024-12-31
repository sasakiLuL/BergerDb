using BergerDb.Application.Abstractions.Data;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Emails;
using BergerDb.Domain.PaymentProcesses;
using BergerDb.Domain.PdfTemplates;
using BergerDb.Persistanse.Customers;
using BergerDb.Persistanse.Emails;
using BergerDb.Persistanse.PaymentProcesses;
using BergerDb.Persistanse.PdfTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BergerDb.Persistanse;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistanseLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<BergerDbContext>(options =>
        {
            options.UseSqlite(connectionString, opt =>
            {
                opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IEmailRepository, EmailRepository>();

        services.AddScoped<IPaymentProcessRepository, PaymentProcessRepository>();

        services.AddScoped<IPdfTemplateRepository, PdfTemplateRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
