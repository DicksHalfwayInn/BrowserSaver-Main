



using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFAppBSUI.ValueConverters
{
    public class ColumnSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSpan = (bool)value;
            return isSpan ? 2 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
