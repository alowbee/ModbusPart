using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class FuncToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            else if ((int)value < 0) { return false; }
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
