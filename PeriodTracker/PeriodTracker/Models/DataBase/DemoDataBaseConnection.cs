namespace PeriodTracker
{
    internal class DemoDataBaseConnection : DataBaseConnectionBase
    {

        public DemoDataBaseConnection() : base("DemoDataBase.db")
        {
            Task.Run(async() => { await InitializeDemoTable(); }).Wait();
        }

        private async Task InitializeDemoTable()
        {
            DataBaseConnection.DeleteAll<PeriodItem>();
            DataBaseConnection.DropTable<PeriodItem>();
            DataBaseConnection.CreateTable<PeriodItem>();
            await Insert(new PeriodItem(new DateTime(2022,11,3), 0));
            await Insert(new PeriodItem(new DateTime(2022, 12, 2), 29));
            await Insert(new PeriodItem(new DateTime(2022, 12, 29), 27));
            await Insert(new PeriodItem(new DateTime(2023, 01, 26), 28));
            await Insert(new PeriodItem(new DateTime(2023, 02, 23), 28));
            await Insert(new PeriodItem(new DateTime(2023, 03, 23), 28));
            await Insert(new PeriodItem(new DateTime(2023, 04, 22), 29));
            await Insert(new PeriodItem(new DateTime(2023, 05, 20), 28));
        }
    }
}
