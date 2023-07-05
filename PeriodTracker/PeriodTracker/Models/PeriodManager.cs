using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodTracker
{
    internal class PeriodManager : IPeriodManager
    {
        private readonly IDataBaseManager _dataBaseManager;
        private IDataBaseConnection _connection;

        public event EventHandler<EventArgs> StatisticsChanged;
        public DateTime TimeOfLastPeriod { get; private set; }

        public DateTime TimeOfNextNominalPeriod { get; private set; }

        public DateTime TimeOfNextPersonalizedPeriod => throw new NotImplementedException();

        public int RemainingNominalDays { get; private set; }

        public int RemainingPersonalizedDays => throw new NotImplementedException();

        public int NominalPeriodFrequency => 28;

        public int PersonalizedPeriodFrequency => throw new NotImplementedException();

        public PeriodManager(IDataBaseManager dataBaseManager)
        {
            _dataBaseManager = dataBaseManager;
            _dataBaseManager.DemoModeChanged += DataBaseManager_DemoModeChanged;
            RunStatisticsInTask();
        }

        private void DataBaseManager_DemoModeChanged(object sender, EventArgs e)
        {
            RunStatisticsInTask();
        }
        public async Task RunStatistics()
        {
            _connection = _dataBaseManager.GetDataBaseConnection();
            var periodItems = await _connection.GetTable();

            TimeOfLastPeriod = periodItems.OrderByDescending(_ => _.StartTime).FirstOrDefault()?.StartTime ?? DateTime.MinValue;
            TimeOfNextNominalPeriod = TimeOfLastPeriod == DateTime.MinValue ? DateTime.MinValue : TimeOfLastPeriod + TimeSpan.FromDays(NominalPeriodFrequency);
            RemainingNominalDays = TimeOfNextNominalPeriod == DateTime.MinValue ? int.MinValue : (TimeOfNextNominalPeriod - DateTime.Today).Days;

            StatisticsChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task SaveDate(DateTime date)
        {
            var elapsedDays = TimeOfLastPeriod == DateTime.MinValue ? 0 : (date - TimeOfLastPeriod).Days;
            await _connection.Insert(new PeriodItem(date, elapsedDays));
            await RunStatistics();
        }

        public async Task<IEnumerable<PeriodItem>> GetHistoricalPeriodItems()
        {
            var periodItems = await _dataBaseManager.GetDataBaseConnection().GetTable();
            return periodItems.ToList();
        }

        public IEnumerable<DateTime> GetNominalFutureDates(int count)
        {
            List<DateTime> futureDates = new List<DateTime>();

            if(TimeOfNextNominalPeriod == DateTime.MinValue)
            {
                return futureDates;
            }

            for (int i = 0; i < count-1; i++)
            {
                var futureDate = TimeOfNextNominalPeriod + TimeSpan.FromDays( i * NominalPeriodFrequency);
                futureDates.Add(futureDate);
            }

            return futureDates;
        }
        public IEnumerable<DateTime> GetPersonalizedFutureDates(int count)
        {
            throw new NotImplementedException();
        }

        private void RunStatisticsInTask()
        {
            Task.Run(async () => await RunStatistics()).Wait();
        }
    }
}
