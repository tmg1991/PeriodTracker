using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeriodTracker
{
    public class ViewModelBase : ObservableObject, INotifyPropertyChanged
    {
        protected readonly IDataBaseManager DataBaseManager;

        private bool _isAppInDemoMode;
        public bool IsAppInDemoMode
        {
            get { return _isAppInDemoMode; }
            set { _isAppInDemoMode = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase(IDataBaseManager dataBaseManager)
        {
            DataBaseManager = dataBaseManager;
            DataBaseManager.DemoModeChanged += DataBaseManager_DemoModeChanged;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnAppearing()
        {
            IsAppInDemoMode = DataBaseManager.IsAppInDemoMode();
        }

        private void DataBaseManager_DemoModeChanged(object sender, EventArgs e)
        {
            OnAppearing();
        }
    }
}
