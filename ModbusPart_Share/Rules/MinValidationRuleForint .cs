using System;
using System.Globalization;
using System.Windows.Controls;

namespace ModbusPart.Rules
{
    public class MinValidationRuleForint : ValidationRule
    {
        public int Min { get; set; }

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
