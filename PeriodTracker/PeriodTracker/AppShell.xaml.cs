namespace PeriodTracker;

public partial class AppShell : Shell
{
    public string AppVersion { get; set; }
    public AppShell()
	{
		var version = "1.0.0";
		AppVersion = $"Period Tracker v{version}";
		InitializeComponent();
		BindingContext = this;
	}
}
