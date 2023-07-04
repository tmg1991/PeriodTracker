using CommunityToolkit.Mvvm.Input;

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

        private int _remainingDays;

        public int RemainingDays
        {
            get { return _remainingDays; }
            set { _remainingDays = value; NotifyPropertyChanged(); }
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
            await PeriodManager.SaveDate(DateTime.Today);
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
        }

    }
}
