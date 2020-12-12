namespace DocJc.Converters
{
    using System.Globalization;
    using Xamarin.Forms;
    using System;

    public class PositiveIntegerToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            var numericValue = (int) value;
            return numericValue > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
