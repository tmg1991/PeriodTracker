using SQLite;

namespace PeriodTracker
{
    internal abstract class DataBaseConnectionBase : IDataBaseConnection
    {
        private SQLiteConnection _dataBaseConnection;
        protected SQLiteConnection DataBaseConnection => _dataBaseConnection;
        protected readonly string Filename;

        public DataBaseConnectionBase(string filename)
        {
            Filename = filename;
            var dataBasePath = Path.Join(FileSystem.AppDataDirectory, Filename);
            _dataBaseConnection = new SQLiteConnection(dataBasePath);
            DataBaseConnection.CreateTable<PeriodItem>();
        }
        public async Task Insert(IPeriodItem periodItem)
        {
            var persistedItems = await GetTable();
            if (persistedItems?.ToArray() != null && persistedItems.Any(_ => _.StartTime == periodItem.StartTime))
            {
                return;
            }
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

        public async Task UpdateItem(IPeriodItem periodItem)
        {
            await Task.Run(() =>
            {
                DataBaseConnection.Update(periodItem);
            });
        }

        public async Task Remove(IPeriodItem periodItem)
        {
            await Task.Run(() => {
                DataBaseConnection.Delete(periodItem);
            });
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dataBaseConnection != null)
                {
                    _dataBaseConnection.Dispose();
                    _dataBaseConnection = null;
                }
            }
        }
    }
}
