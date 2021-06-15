using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class DataBitSelectedIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int content = (int)value;
            switch (content)
            {
                case 7:
                    return 0;
                case 8:
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
                    return 7;
                case 1:
                    return 8;
                default:
                    return null;
            }
        }
    }
}
