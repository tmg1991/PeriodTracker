namespace PeriodTracker;

public partial class FutureDatesPage : ContentPage
{
	private readonly FutureDatesViewModel _viewModel;
	public FutureDatesPage(FutureDatesViewModel viewModel)
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