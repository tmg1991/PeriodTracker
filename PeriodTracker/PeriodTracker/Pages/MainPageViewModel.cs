using CommunityToolkit.Mvvm.Input;
using PeriodTracker.Resources;

namespace PeriodTracker
{
    public partial class MainPageViewModel : ViewModelBase
    {
        private DateTime _lastOccasion;
        public DateTime LastOccasion
        {
            get { return _lastOccasion; }
            set { _lastOccasion = value; NotifyPropertyChanged(); }
        }

        private DateTime _nextOccasion;
        public DateTime NextOccasion
        {
            get { return _nextOccasion; }
            set { _nextOccasion = value; NotifyPropertyChanged(); }
        }

        private DateTime _nextPersonalizedOccasion;
        public DateTime NextPersonalizedOccasion
        {
            get { return _nextPersonalizedOccasion; }
            set { _nextPersonalizedOccasion = value; NotifyPropertyChanged(); }
        }

        private int _remainingDays;
        public int RemainingDays
        {
            get { return _remainingDays; }
            set { _remainingDays = value; NotifyPropertyChanged(); }
        }

        private int _remainingPersonalizedDays;
        public int RemainingPersonalizedDays
        {
            get { return _remainingPersonalizedDays; }
            set { _remainingPersonalizedDays = value; NotifyPropertyChanged(); }
        }

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
