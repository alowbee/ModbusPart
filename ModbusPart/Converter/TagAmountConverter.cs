using System;
using System.Globalization;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class TagAmountConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var readmapcount = (int)values[0] - 1;
            var writecount= (int)values[1] - 1;
            return (readmapcount + writecount).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
