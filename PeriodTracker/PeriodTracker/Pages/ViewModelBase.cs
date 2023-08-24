using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace PeriodTracker
{
    public partial class ViewModelBase : ObservableObject, INotifyPropertyChanged
    {
        protected readonly IDataBaseManager DataBaseManager;
        protected readonly IPeriodManager PeriodManager;

        [ObservableProperty]
        private bool _isAppInDemoMode;
        
        public ViewModelBase(IDataBaseManager dataBaseManager, IPeriodManager periodManager)
        {
            DataBaseManager = dataBaseManager;
            PeriodManager = periodManager;
            DataBaseManager.DemoModeChanged += DataBaseManager_DemoModeChanged;
        }

        public virtual void OnAppearing()
        {
            IsAppInDemoMode = DataBaseManager.IsAppInDemoMode();
            Task.Run(PeriodManager.RunStatistics).Wait();
        }

        private void DataBaseManager_DemoModeChanged(object sender, EventArgs e)
        {
            OnAppearing();
        }
    }
}
