using BergerDb.Application;
using BergerDb.Domain;
using BergerDb.Persistanse;
using Microsoft.Extensions.Logging;
using System.Reflection;
using BergerDb.UI.Pages.MainNavigation;
using BergerDb.Controls.PaginationView;
using BergerDb.UI.Pages.MainPages;
using BergerDb.UI.Pages.Customers;

namespace BergerDb.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            /*.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(SelectableLabel), typeof(SelectableLabelHandler));
            })*/;

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddDomainLayer();

        builder.Services.AddApplicationLayer();

        builder.Services.AddPersistanseLayer("Data Source=berger.db;");

        builder.Services.AddTransient<MainNavigationShellModel>();

        builder.Services.AddSingleton<MainNavigationShell>();

        builder.Services.AddTransient<PaginationModel>();

        builder.Services.AddTransient<CustomerPageModel>();

        builder.Services.AddSingleton<CustomerPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        using IServiceScope serviceScope = app.Services.CreateScope();

        using BergerDbContext context =
            serviceScope.ServiceProvider.GetRequiredService<BergerDbContext>();

        //context.Database.Migrate();

        return app;
    }
}
