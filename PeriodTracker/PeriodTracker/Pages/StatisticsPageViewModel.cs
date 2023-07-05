using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodTracker
{
    public partial class StatisticsPageViewModel : ViewModelBase
    {
        private double _average;
        public double Average
        {
            get { return _average; }
            set { _average = value; NotifyPropertyChanged(); }
        }

        private double _stdDeviation;
        public double StdDeviation
        {
            get { return _stdDeviation; }
            set { _stdDeviation = value; NotifyPropertyChanged(); }
        }

        private int _minimum;
        public int Minimum
        {
            get { return _minimum; }
            set { _minimum = value; NotifyPropertyChanged(); }
        }

        private int _maximum;
        public int Maximum
        {
            get { return _maximum; }
            set { _maximum = value; NotifyPropertyChanged(); }
        }

        private int _range;
        public int Range
        {
            get { return _range; }
            set { _range = value; NotifyPropertyChanged(); }
        }

        public StatisticsPageViewModel(IDataBaseManager dataBaseManager, IPeriodManager periodManager) : base(dataBaseManager, periodManager)
        {
            PeriodManager.StatisticsChanged += PeriodManager_StatisticsChanged;
            Update();
        }

        private void PeriodManager_StatisticsChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {
            Average = PeriodManager.Average;
            StdDeviation = PeriodManager.StdDeviation;
            Minimum = PeriodManager.Minimum;
            Maximum = PeriodManager.Maximum;
            Range = PeriodManager.Range;
        }
    }
}
