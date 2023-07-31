using SQLite;

namespace PeriodTracker
{
    public interface IDataBaseConnection
    {
        Task Insert(IPeriodItem periodItem);
        Task Remove(IPeriodItem periodItem);
        Task<TableQuery<PeriodItem>> GetTable();
        Task UpdateItem(IPeriodItem periodItem);
    }
}
