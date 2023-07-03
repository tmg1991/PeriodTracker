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

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<ComplementPage>();
        builder.Services.AddSingleton<ComplementPageViewModel>();

        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<HistoryPageViewModel>();

        builder.Services.AddSingleton<FutureDatesPage>();
        builder.Services.AddSingleton<FutureDatesViewModel>();

        builder.Services.AddTransient<DemoPage>();
		builder.Services.AddTransient<DemoPageViewModel>();

		return builder.Build();
	}
}
