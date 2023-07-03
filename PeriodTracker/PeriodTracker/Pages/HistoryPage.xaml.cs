namespace PeriodTracker;

public partial class HistoryPage : ContentPage
{
	private readonly HistoryPageViewModel _viewModel;
	public HistoryPage(HistoryPageViewModel viewModel)
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