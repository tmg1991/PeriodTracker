using CommunityToolkit.Mvvm.Input;

namespace PeriodTracker
{
    public partial class DemoPageViewModel : ViewModelBase
    {

        public DemoPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            
        }

        [RelayCommand]
        public async Task SetDemoDataBase()
        {
            await DataBaseManager.SetDemoDataBaseConnection();
        }
    }
}
