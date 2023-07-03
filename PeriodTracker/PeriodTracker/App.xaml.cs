namespace PeriodTracker;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);

        window.Deactivated += (s, e) =>
        {
            var dataBaseManager = Handler.MauiContext.Services.GetService<IDataBaseManager>();
            dataBaseManager.SetProductionDataBaseConnection();
        };

        return window;
    }
}
