using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = (int)value;
            var a =new[] { 1, 2, 3 };
        
            return System.Convert.ToInt32(temp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = value as string;
            return System.Convert.ToInt32(temp, 10);
        }
    }
}
