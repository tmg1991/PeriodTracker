using System.Globalization;

namespace PeriodTracker
{
    internal class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime)
            {
                var dateTime = (DateTime)value;
                if(dateTime == DateTime.MinValue)
                {
                    return "-";
                }
                return dateTime.ToString("yyyy/MMMM/dd");
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
