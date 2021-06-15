using System;
using System.Globalization;
using System.Windows.Controls;

namespace ModbusPart.Rules
{
    public class HexRangValidationRule : ValidationRule
    {
        public int Max { get; set; }
        public int Min { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var address = value as string;
            var Max_H = Convert.ToString(Max, 16).ToUpper();
            var Min_H = Convert.ToString(Min, 16).ToUpper();
            var address_D = Convert.ToInt32(address, 16);

            if (address_D > Max || address_D < Min)
            {

                return new ValidationResult(false, $"输入最大值为{Max_H},最小值为{Min_H},重新输入");
            }
            else
                return new ValidationResult(true, null);

        }
    }
}
