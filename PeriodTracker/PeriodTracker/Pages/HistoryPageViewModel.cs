using System.Collections.ObjectModel;

namespace PeriodTracker
{
    public class HistoryPageViewModel : ViewModelBase
    {
        public ObservableCollection<IPeriodItem> PeriodItems { get; set; } = new ObservableCollection<IPeriodItem>();

        public HistoryPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            Load();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            Load();
        }

        private void Load()
        {
            PeriodItems.Clear();
            IEnumerable<PeriodItem> periodItems = null;
            Task.Run(async () => { 
                periodItems = await PeriodManager.GetHistoricalPeriodItems();
            }).Wait();

            foreach (var item in periodItems)
            {
                PeriodItems.Add(item);
            }
        }
    }
}
