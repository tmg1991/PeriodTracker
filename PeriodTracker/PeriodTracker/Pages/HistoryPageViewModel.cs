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

        private async Task Load()
        {
            PeriodItems.Clear();
            var periodItems = await PeriodManager.GetHistoricalPeriodItems();

            foreach (var item in periodItems)
            {
                PeriodItems.Add(item);
            }
        }
    }
}
