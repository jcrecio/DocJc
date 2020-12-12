namespace DocJc.Converters
{
    using System.Globalization;
    using Xamarin.Forms;
    using System;

    public class InverseConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
