namespace PeriodTracker
{
    public interface IPeriodManager
    {
        DateTime TimeOfLastPeriod { get; }
        DateTime TimeOfNextNominalPeriod { get; }
        DateTime TimeOfNextPersonalizedPeriod { get; }
        int RemainingNominalDays { get; }
        int RemainingPersonalizedDays { get; }
        int NominalPeriodFrequency { get; }
        int PersonalizedPeriodFrequency { get; }
        double Average { get; }
        double StdDeviation { get; }
        int Minimum { get; }
        int Maximum { get; }
        int Range { get; }
        Task SaveDate(DateTime date);
        Task RunStatistics();
        IEnumerable<PeriodItem> GetHistoricalPeriodItems();
        IEnumerable<DateTime> GetNominalFutureDates(int count);
        IEnumerable<DateTime> GetPersonalizedFutureDates(int count);
        event EventHandler<EventArgs> StatisticsChanged;
    }
}
