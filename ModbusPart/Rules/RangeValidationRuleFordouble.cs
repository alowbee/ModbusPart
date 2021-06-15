using System;
using System.Globalization;
using System.Windows.Controls;

namespace ModbusPart.Rules
{
    public class RangeValidationRuleFordouble : ValidationRule
    {
        public double Min { get; set; }
        public double Max { get; set; }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double retryvalue = 0;
            try
            {
                if (((string)value).Length > 0)
                    retryvalue = Convert.ToDouble((String)value);
            }
            catch
            {
                return new ValidationResult(false, "输入应当为整数，当前类型错误");
            }
            if (Min > Max)
            {
                return new ValidationResult(false,
                      "最小值不应该大于最大值");
            }

            else if ((retryvalue < Min) || (retryvalue > Max))
            {
                return new ValidationResult(false,
                  "输入范围: " + Min + " - " + Max + "之间");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
