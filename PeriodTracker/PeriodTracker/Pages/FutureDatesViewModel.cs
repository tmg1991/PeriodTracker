using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace PeriodTracker
{
    public partial class FutureDatesViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<DateTime> _futureDates;

        [ObservableProperty]
        private ObservableCollection<DateTime> _personalizedFutureDates;

        public FutureDatesViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            UpdateFutureDates();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            UpdateFutureDates();
        }

        private void UpdateFutureDates()
        {
            var futureMonths =  12;
            FutureDates = new ObservableCollection<DateTime>(PeriodManager.GetNominalFutureDates(futureMonths));
            PersonalizedFutureDates = new ObservableCollection<DateTime>(PeriodManager.GetPersonalizedFutureDates(futureMonths));
        }
    }
}
