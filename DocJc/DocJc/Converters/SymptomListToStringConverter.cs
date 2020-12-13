namespace DocJc.Converters
{
    using System.Globalization;
    using Xamarin.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.ViewModel;

    public class SymptomListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IList<BaseEntityViewModel> list) || !list.Any())
            {
                return new List<BaseEntityViewModel>
                {
                    new BaseEntityViewModel
                    {
                        Name = "No symptoms selected"
                    }
                };
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
