using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PeriodTracker.Resources;

namespace PeriodTracker
{
    public partial class MainPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DateTime? _lastOccasion;

        [ObservableProperty]
        private DateTime? _nextOccasion;

        [ObservableProperty]
        private DateTime? _nextPersonalizedOccasion;

        [ObservableProperty]
        private int? _remainingDays;

        [ObservableProperty]
        private int? _remainingPersonalizedDays;

        public MainPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            PeriodManager.StatisticsChanged += PeriodManager_StatisticsChanged;
        }

        private void PeriodManager_StatisticsChanged(object sender, EventArgs e)
        {
            UpdateValues();
        }

        [RelayCommand]
        public async Task SaveToday()
        {
            var date = DateTime.Today;
            await PeriodManager.SaveDate(date);
            await Shell.Current.DisplayAlert(date.ToString("yyyy'-'MMMM'-'dd"), AppRes.SavedDialogMessage, AppRes.DialogButton);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            UpdateValues();
        }

        private void UpdateValues()
        {
            LastOccasion = PeriodManager.TimeOfLastPeriod;
            NextOccasion = PeriodManager.TimeOfNextNominalPeriod;
            RemainingDays = PeriodManager.RemainingNominalDays;

            NextPersonalizedOccasion = PeriodManager.TimeOfNextPersonalizedPeriod;
            RemainingPersonalizedDays = PeriodManager.RemainingPersonalizedDays;
        }

    }
}
