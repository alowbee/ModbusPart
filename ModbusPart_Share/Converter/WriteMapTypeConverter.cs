using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class WriteMapTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int func = (int)value;
                if (func % 5 == 0)

                    return "M";
                else if (func % 5 == 1)
                    return "D";
                else return null;
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
