using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModbusPart.Converter
{
    public class WriteMapFuncConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return -1;
            else
            {
                var func_ini = (int)value;
                if (func_ini % 5 < 2)
                    return func_ini % 5;
                else
                    return -1;
            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           // return (int)value+5;
            return (int)value +5;
        }
    }
}
