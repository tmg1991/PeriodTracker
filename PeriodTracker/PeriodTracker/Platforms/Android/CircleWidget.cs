using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

//namespace Maui.Widgets;
namespace PeriodTracker.Platforms.Android;

[BroadcastReceiver(Label = "@string/widget_circle", Exported = true)]
[IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
[MetaData("android.appwidget.provider", Resource = "@xml/widget_circle")]
[Service(Exported = true)]
public class CircleWidget : AppWidgetProvider
{
    public const string TAG = "CircleWidget";

    public override void OnAppWidgetOptionsChanged(Context? context, AppWidgetManager? appWidgetManager, int appWidgetId,
        Bundle? newOptions)
    {
        var minWidth = newOptions.GetInt(AppWidgetManager.OptionAppwidgetMinWidth);
        var maxWidth = newOptions.GetInt(AppWidgetManager.OptionAppwidgetMaxWidth);
        var minHeight = newOptions.GetInt(AppWidgetManager.OptionAppwidgetMinHeight);
        var maxHeight = newOptions.GetInt(AppWidgetManager.OptionAppwidgetMaxHeight);

        var updateViews = UpdateViews(context);

        UpdateVisiblity(updateViews, minWidth, maxHeight, maxWidth);

        appWidgetManager.UpdateAppWidget(appWidgetId, updateViews);

        base.OnAppWidgetOptionsChanged(context, appWidgetManager, appWidgetId, newOptions);
    }

    public override void OnEnabled(Context? context)
    {
        var updateViews = UpdateViews(context);
        base.OnEnabled(context);
    }

    public override void OnRestored(Context context, int[] oldWidgetIds, int[] newWidgetIds)
    {
        var updateViews = UpdateViews(context);
        base.OnRestored(context, oldWidgetIds, newWidgetIds);
    }

    public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
    {
        foreach (var widgetId in appWidgetIds)
        {
            var updateViews = UpdateViews(context);
            appWidgetManager.UpdateAppWidget(widgetId, updateViews);
        }
    }

    private void UpdateVisiblity(RemoteViews updateViews, int minWidth, int maxHeight, int maxWidth)
    {
        updateViews.SetViewVisibility(Resource.Id.layout_1_1, ViewStates.Gone);
        updateViews.SetViewVisibility(Resource.Id.layout_2_1, ViewStates.Gone);
        updateViews.SetViewVisibility(Resource.Id.layout_2_2, ViewStates.Gone);
        updateViews.SetViewVisibility(Resource.Id.layout_3_2, ViewStates.Gone);
        updateViews.SetViewVisibility(Resource.Id.layout_3_3, ViewStates.Gone);
        updateViews.SetViewVisibility(Resource.Id.layout_4_4, ViewStates.Gone);
        updateViews.SetViewVisibility(Resource.Id.layout_5_5, ViewStates.Gone);

        if (minWidth <= 130) // 1x1, 1x2, 1x3, 1x4, 1x5
        {
            updateViews.SetViewVisibility(Resource.Id.layout_1_1, ViewStates.Visible);
        }
        else if (minWidth > 130 && maxHeight <= 104) // 2x1, 3x1, 4x1, 5x1
        {
            updateViews.SetViewVisibility(Resource.Id.layout_2_1, ViewStates.Visible);
        }
        else if (maxWidth <= 277) // 2x2, 2x3, 2x4, 2x5
        {
            updateViews.SetViewVisibility(Resource.Id.layout_2_2, ViewStates.Visible);
        }
        else if (minWidth > 134 && maxHeight <= 225) // 3x2, 4x2, 5x2
        {
            updateViews.SetViewVisibility(Resource.Id.layout_3_2, ViewStates.Visible);
        }
        else if (maxWidth <= 424) // 3x3, 3x4, 3x5
        {
            updateViews.SetViewVisibility(Resource.Id.layout_3_3, ViewStates.Visible);
        }
        else if (maxWidth <= 571) // 4x3, 4x4, 4x5
        {
            updateViews.SetViewVisibility(Resource.Id.layout_4_4, ViewStates.Visible);
        }
        else // 5x3, 5x4, 5x5
        {
            updateViews.SetViewVisibility(Resource.Id.layout_5_5, ViewStates.Visible);
        }
    }

    private RemoteViews UpdateViews(Context context)
    {
        var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_circle);
        var periodManager = App.Current.Handler.MauiContext.Services.GetService<IPeriodManager>();
        if (periodManager == null)
        {
            return updateViews;
        }

        Task.Run(() => { periodManager.RunStatistics(); }).Wait();
        var remainingNominalString = periodManager.RemainingNominalDays == int.MinValue ? "-" : periodManager.RemainingNominalDays.ToString();
        var remainingPersonalizedString = periodManager.RemainingPersonalizedDays == int.MinValue ? "-" : periodManager.RemainingPersonalizedDays.ToString();

        var remainingString = $"{remainingNominalString} | {remainingPersonalizedString}";
        updateViews.SetTextViewText(Resource.Id.remaining, remainingString);
        updateViews.SetTextViewText(Resource.Id.remaining_2_1, remainingString);
        updateViews.SetTextViewText(Resource.Id.remaining_2_2, remainingString);
        updateViews.SetTextViewText(Resource.Id.remaining_3_2, remainingString);
        updateViews.SetTextViewText(Resource.Id.remaining_3_3, remainingString);
        updateViews.SetTextViewText(Resource.Id.remaining_4_4, remainingString);
        updateViews.SetTextViewText(Resource.Id.remaining_5_5, remainingString);
        
        return updateViews;
    }

}