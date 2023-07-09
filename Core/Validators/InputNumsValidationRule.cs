using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Core.Validators
{
    public class InputNumsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringData = value as string;
            if (stringData == null)
                return new ValidationResult(false, "not a string");
            if (int.TryParse(stringData, out int dummy))
                return new ValidationResult(true, "not an integer");
            return ValidationResult.ValidResult;
        }
    }
}
