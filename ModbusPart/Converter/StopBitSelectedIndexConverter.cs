using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class StopBitSelectedIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int content = (int)value;
            switch (content)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                default:
                    return -1;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int index = (int)value;
            switch (index)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                default:
                    return null;
            }
        }
    }
}
