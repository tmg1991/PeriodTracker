using SQLite;

namespace PeriodTracker
{
    public interface IDataBaseConnection
    {
        Task Insert(IPeriodItem periodItem);
        Task<TableQuery<PeriodItem>> GetTable();
    }
}
