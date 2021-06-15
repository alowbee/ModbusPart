using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class BaudrateSelectedIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int content = (int)value;
            switch (content)
            {
                case 4800:
                    return 0;
                case 9600:
                    return 1;
                case 19200:
                    return 2;
                case 38400:
                    return 3;
                case 57600:
                    return 4;
                case 115200:
                    return 5;
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
                    return 4800;
                case 1:
                    return 9600;
                case 2:
                    return 19200;
                case 3:
                    return 38400;
                case 4:
                    return 57600;
                case 5:
                    return 115200;
                default:
                    return null;
            }
        }
    }
}
