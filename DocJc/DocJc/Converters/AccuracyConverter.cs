namespace DocJc.Converters
{
    using System.Globalization;
    using Xamarin.Forms;
    using System;

    public class AccuracyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "(NA)";
            }

            return $"({value}%)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
