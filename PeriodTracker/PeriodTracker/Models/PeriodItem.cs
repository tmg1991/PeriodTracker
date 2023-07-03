using SQLite;

namespace PeriodTracker
{
    public class PeriodItem : IPeriodItem
    {
        [PrimaryKey, Unique]
        public DateTime StartTime { get; set; }
        public int ElapsedDays { get; set; }

        public PeriodItem()
        {
            // used for database
        }
        public PeriodItem(DateTime startTime, int elapsedDays)
        {
            StartTime = startTime;
            ElapsedDays = elapsedDays;
        }
    }
}
