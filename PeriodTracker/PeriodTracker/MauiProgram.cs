using Microsoft.Extensions.Logging;
using PeriodTracker;

namespace PeriodTracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IDataBaseManager, DataBaseManager>();
		builder.Services.AddSingleton<IPeriodManager, PeriodManager>();

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<ComplementPage>();
        builder.Services.AddTransient<ComplementPageViewModel>();

        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<HistoryPageViewModel>();

        builder.Services.AddTransient<FutureDatesPage>();
        builder.Services.AddTransient<FutureDatesViewModel>();
        
		builder.Services.AddTransient<StatisticsPage>();
        builder.Services.AddTransient<StatisticsPageViewModel>();

        builder.Services.AddTransient<DemoPage>();
		builder.Services.AddTransient<DemoPageViewModel>();

		return builder.Build();
	}
}
