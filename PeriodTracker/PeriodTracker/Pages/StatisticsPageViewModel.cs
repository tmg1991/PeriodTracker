using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace PeriodTracker
{
    public partial class StatisticsPageViewModel : ViewModelBase
    {
        private ISeries[] _series;
        public ISeries[] Series
        {
            get { return _series; }
            set { _series = value; NotifyPropertyChanged(); }
        }

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

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateChart();
        }

        private async void PeriodManager_StatisticsChanged(object sender, EventArgs e)
        {
            Update();
            await UpdateChart();
        }

        private void Update()
        {
            Average = PeriodManager.Average;
            StdDeviation = PeriodManager.StdDeviation;
            Minimum = PeriodManager.Minimum;
            Maximum = PeriodManager.Maximum;
            Range = PeriodManager.Range;
        }

        private async Task UpdateChart()
        {
            var historicalItems = await PeriodManager.GetHistoricalPeriodItems();
            var historicalPeriodLengths = historicalItems
                .Where(_ => _.ElapsedDays != 0)
                .Select(_ => (double)_.ElapsedDays)
                .ToArray();
            Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = historicalPeriodLengths,
                Fill = null,
                Stroke = new SolidColorPaint(new SKColor(252,98,193)) { StrokeThickness = 6 },
                GeometryStroke = new SolidColorPaint(new SKColor(204,34,242)) { StrokeThickness = 6 }
            }
        };
        }
    }
}
