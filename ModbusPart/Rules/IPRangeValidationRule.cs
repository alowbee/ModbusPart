using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ModbusPart.Rules
{
    public class IPRangeValidationRule : ValidationRule
    {
        private Regex IPregex = new Regex("^(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\."
                + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."
                + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."
                + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            if (string.IsNullOrEmpty(input))
                return new ValidationResult(false, Properties.Lang.Lang.IPisNull);
            else if (IPregex.IsMatch(input))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, Properties.Lang.Lang.NotCorrectIP);
            }

        }
    }
}
