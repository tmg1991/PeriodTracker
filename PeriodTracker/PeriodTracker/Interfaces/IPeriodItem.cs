namespace PeriodTracker
{
    public interface IPeriodItem
    {
        DateTime StartTime { get; set; }
        int ElapsedDays { get; set; }
    }
}
