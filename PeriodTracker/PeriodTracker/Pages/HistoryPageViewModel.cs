using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace PeriodTracker
{
    public partial class HistoryPageViewModel : ViewModelBase
    {
        public ObservableCollection<IPeriodItem> PeriodItems { get; set; } = new ObservableCollection<IPeriodItem>();

        public HistoryPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            Load();
        }

        [RelayCommand]
        public async Task RemoveDate(object o)
        {
            if(o is not PeriodItem periodItem)
            {
                return;
            }

            bool answer = await Application.Current.MainPage.DisplayAlert(Resources.AppRes.DeleteDateTitle, Resources.AppRes.DeleteDateQuestion, Resources.AppRes.Yes, Resources.AppRes.No);
            if (!answer)
            {
                return;
            }

            await PeriodManager.RemoveDate(periodItem);
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
            IEnumerable<IPeriodItem> periodItems = null;
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
