namespace PeriodTracker;

public partial class StatisticsPage : ContentPage
{
	private readonly StatisticsPageViewModel _viewModel;
	public StatisticsPage(StatisticsPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_viewModel.OnAppearing();
    }
}