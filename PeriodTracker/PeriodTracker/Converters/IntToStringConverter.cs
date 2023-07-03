using System.Globalization;

namespace PeriodTracker
{
    internal class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var dateTime = (int)value;
                if (dateTime == int.MinValue)
                {
                    return "-";
                }
                return dateTime.ToString();
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
