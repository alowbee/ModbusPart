using System;
using System.Globalization;
using System.Windows.Controls;

namespace ModbusPart.Rules
{
    public class RangeValidationRuleForint : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int retryvalue = 0;
            try
            {
                if (((string)value).Length > 0)
                    retryvalue = Convert.ToInt32((String)value);
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
