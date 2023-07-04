using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            FutureDates = new ObservableCollection<DateTime>(PeriodManager.GetNominalFutureDates(12));
        }
    }
}
