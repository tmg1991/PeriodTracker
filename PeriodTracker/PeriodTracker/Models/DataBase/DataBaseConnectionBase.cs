using SQLite;

namespace PeriodTracker
{
    internal abstract class DataBaseConnectionBase : IDataBaseConnection
    {
        protected readonly SQLiteConnection DataBaseConnection;
        protected readonly string Filename;

        public DataBaseConnectionBase(string filename)
        {
            Filename = filename;
            var dataBasePath = Path.Join(FileSystem.AppDataDirectory, Filename);
            DataBaseConnection = new SQLiteConnection(dataBasePath);
            DataBaseConnection.CreateTable<PeriodItem>();
        }
        public async Task Insert(IPeriodItem periodItem)
        {
            await Task.Run(() => { 
                DataBaseConnection.Insert(periodItem);
            });
        }

        public async Task<TableQuery<PeriodItem>> GetTable()
        {
            TableQuery<PeriodItem> periodItems = 
                await Task.Factory.StartNew(() =>
                {
                    return DataBaseConnection.Table<PeriodItem>();
                });
            return periodItems;
        }
    }
}
