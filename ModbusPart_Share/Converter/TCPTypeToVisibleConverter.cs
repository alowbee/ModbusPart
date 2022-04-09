using ModbusPart.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class TCPTypeToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SlaveType type = (SlaveType)value;
            switch (type)
            {
                case SlaveType.TCP:
                    return Visibility.Visible;

                case SlaveType.Serials:
                    return Visibility.Collapsed;
                default:
                    return Visibility.Visible;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
