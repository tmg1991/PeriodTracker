using CommunityToolkit.Mvvm.Input;

namespace PeriodTracker
{
    public partial class DemoPageViewModel : ViewModelBase
    {

        public DemoPageViewModel(IDataBaseManager dataBaseManager) : base(dataBaseManager)
        {
            
        }

        [RelayCommand]
        public async Task SetDemoDataBase()
        {
            await DataBaseManager.SetDemoDataBaseConnection();
        }
    }
}
