using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PeriodTracker.Resources;

namespace PeriodTracker
{
    public partial class ComplementPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DateTime _maximumDisplayedDate;

        [ObservableProperty]
        private DateTime _selectedDate;

        public ComplementPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            MaximumDisplayedDate = DateTime.Today;
            SelectedDate = DateTime.Today;
        }

        [RelayCommand]
        public async Task SaveDate()
        { 
            await PeriodManager.SaveDate(SelectedDate);
            await Shell.Current.DisplayAlert(SelectedDate.ToString("yyyy'-'MMMM'-'dd"), AppRes.SavedDialogMessage, AppRes.DialogButton);
        }
    }
}
