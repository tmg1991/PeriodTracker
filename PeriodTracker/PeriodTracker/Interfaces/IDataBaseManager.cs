namespace PeriodTracker
{
    public interface IDataBaseManager
    {
        IDataBaseConnection GetDataBaseConnection();
        Task SetDemoDataBaseConnection();
        Task SetProductionDataBaseConnection();
        bool IsAppInDemoMode();
        event EventHandler DemoModeChanged;
    }
}
