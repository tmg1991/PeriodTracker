namespace PeriodTracker;

public partial class DemoPage : ContentPage
{
	private readonly DemoPageViewModel _demoPageViewModel;
	public DemoPage(DemoPageViewModel viewModel)
	{
		InitializeComponent();
		_demoPageViewModel = viewModel;
		BindingContext = _demoPageViewModel;
	}
}