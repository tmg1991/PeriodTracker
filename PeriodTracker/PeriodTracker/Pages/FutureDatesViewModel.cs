using System.Collections.ObjectModel;

namespace PeriodTracker
{
    public partial class FutureDatesViewModel : ViewModelBase
    {
        private ObservableCollection<DateTime> _futureDates;
        public ObservableCollection<DateTime> FutureDates
        {
            get { return _futureDates; }
            set { _futureDates = value; NotifyPropertyChanged(); }
        }
        private ObservableCollection<DateTime> _personalizedFutureDates;
        public ObservableCollection<DateTime> PersonalizedFutureDates
        {
            get { return _personalizedFutureDates; }
            set { _personalizedFutureDates = value; NotifyPropertyChanged(); }
        }



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
