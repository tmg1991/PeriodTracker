using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PeriodTracker
{
    public partial class StatisticsPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ISeries[] _series;

        [ObservableProperty]
        private double _average;

        [ObservableProperty]
        private double _stdDeviation;

        [ObservableProperty]
        private int _minimum;

        [ObservableProperty]
        private int _maximum;

        [ObservableProperty]
        private int _range;

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
