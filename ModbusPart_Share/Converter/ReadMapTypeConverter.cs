using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class ReadMapTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int func = (int)value;
                if (func < 1)
                    return null;
                else if (func < 3)
                    return "M";
                else return "D";
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
