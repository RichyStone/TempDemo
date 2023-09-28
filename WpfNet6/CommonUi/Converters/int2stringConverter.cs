using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfNet6.CommonUi.Converters
{
    public class Int2stringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = value.ToString();
            var val = int.TryParse(obj, out _) ? int.Parse(obj) : 0;
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}