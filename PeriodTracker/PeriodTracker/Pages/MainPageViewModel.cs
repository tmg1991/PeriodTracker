using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodTracker
{
    public partial class MainPageViewModel : ViewModelBase
    {
        private readonly IPeriodManager _periodManager;

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



        public MainPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager)
        {
            _periodManager = periodManager;
            _periodManager.StatisticsChanged += PeriodManager_StatisticsChanged;
        }

        private void PeriodManager_StatisticsChanged(object sender, EventArgs e)
        {
            UpdateValues();
        }

        [RelayCommand]
        public async Task SaveToday()
        {
            await _periodManager.SaveDate(DateTime.Today);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            UpdateValues();
        }

        private void UpdateValues()
        {
            LastOccasion = _periodManager.TimeOfLastPeriod;
            NextOccasion = _periodManager.TimeOfNextNominalPeriod;
            RemainingDays = _periodManager.RemainingNominalDays;
        }

    }
}
