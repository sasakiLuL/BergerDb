using BergerDb.UI.Pages.MainNavigation;

namespace BergerDb.UI;

public partial class App : Microsoft.Maui.Controls.Application
{
    private readonly MainNavigationShell _mainPage;

    public App(MainNavigationShell mainPage)
    {
        InitializeComponent();

        _mainPage = mainPage;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_mainPage);
    }
}