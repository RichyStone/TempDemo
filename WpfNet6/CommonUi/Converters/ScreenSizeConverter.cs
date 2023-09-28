using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfNet6.CommonUi.Converters
{
    public class ScreenSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double size))
                return size * 0.8;
            else
                return 800;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}