using System;
using System.Globalization;
using System.Windows.Controls;

namespace ModbusPart.Rules
{
    public class MinValidationRuleFordouble
        : ValidationRule
    {
        public double Min { get; set; }

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


            if ((retryvalue < Min))
            {
                return new ValidationResult(false,
                  "输入范围应该大于 " + Min);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
