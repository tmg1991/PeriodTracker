﻿using SQLite;
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
        private IEnumerable<PeriodItem> _historicalPeriodItems;

        public event EventHandler<EventArgs> StatisticsChanged;
        public DateTime TimeOfLastPeriod { get; private set; }

        public DateTime TimeOfNextNominalPeriod { get; private set; }

        public DateTime TimeOfNextPersonalizedPeriod => throw new NotImplementedException();

        public int RemainingNominalDays { get; private set; }

        public int RemainingPersonalizedDays => throw new NotImplementedException();

        public int NominalPeriodFrequency => 28;

        public int PersonalizedPeriodFrequency => throw new NotImplementedException();

        public double Average { get; private set; }
        public double StdDeviation { get; private set; }
        public int Minimum{ get; private set; }
        public int Maximum{ get; private set; }
        public int Range { get; private set; }

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
            _historicalPeriodItems = await GetHistoricalPeriodItems();

            TimeOfLastPeriod = _historicalPeriodItems?.OrderByDescending(_ => _.StartTime)?.FirstOrDefault()?.StartTime ?? DateTime.MinValue;
            TimeOfNextNominalPeriod = TimeOfLastPeriod == DateTime.MinValue ? DateTime.MinValue : TimeOfLastPeriod + TimeSpan.FromDays(NominalPeriodFrequency);
            RemainingNominalDays = TimeOfNextNominalPeriod == DateTime.MinValue ? int.MinValue : (TimeOfNextNominalPeriod - DateTime.Today).Days;

            EvaluateStats();

            StatisticsChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task SaveDate(DateTime date)
        {
            var elapsedDays = TimeOfLastPeriod == DateTime.MinValue ? 0 : (date - TimeOfLastPeriod).Days;
            await _connection.Insert(new PeriodItem(date, elapsedDays));
            var items = await GetHistoricalPeriodItems();

            foreach (var item in items)
            {
                if(item.StartTime < date)
                {
                    continue;
                }

                var previousItem = items.TakeWhile(_ => _.StartTime < item.StartTime).LastOrDefault();
                item.ElapsedDays = previousItem == null ? 0 : (item.StartTime - previousItem.StartTime).Days;
                await _connection.UpdateItem(item);
            }
            await RunStatistics();
        }

        public async Task<IEnumerable<PeriodItem>> GetHistoricalPeriodItems()
        {
            return (await _connection.GetTable()).OrderBy(_ => _.StartTime);
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

        private void EvaluateStats()
        {
            Average = _historicalPeriodItems.Any() ? _historicalPeriodItems?.Select(_ => _.ElapsedDays)?.Average() ?? double.NaN : double.NaN;
            StdDeviation = CalculateStdDev(_historicalPeriodItems?.Select(_ => _.ElapsedDays).ToArray());
            var pastElapsedDays = _historicalPeriodItems?.Select(_ => _.ElapsedDays)?.Where(_ => _ != 0)?.ToArray();
            Minimum = (pastElapsedDays != null && pastElapsedDays.Any()) ? pastElapsedDays.Min() : 0;
            Maximum = (pastElapsedDays != null && pastElapsedDays.Any()) ? pastElapsedDays.Max() : 0;
            Range = Maximum - Minimum;

            double CalculateStdDev(IEnumerable<int> numbers)
            {
                if(numbers.Count() < 1)
                {
                    return double.NaN;
                }
                double average = numbers.Average();
                double sumOfSquaresOfDifferences = numbers.Select(_ => (_ - average) * (_ - average)).Sum();
                double sd = Math.Sqrt(sumOfSquaresOfDifferences / (numbers.Count() - 1));
                return sd;
            }
        }
    }
}
