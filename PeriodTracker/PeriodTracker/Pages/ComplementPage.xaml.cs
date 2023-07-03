namespace PeriodTracker;

public partial class ComplementPage : ContentPage
{
	private readonly ComplementPageViewModel _viewModel;
	public ComplementPage(ComplementPageViewModel viewModel)
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