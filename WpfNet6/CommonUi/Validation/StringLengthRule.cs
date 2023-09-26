using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfNet6.CommonUi.Validation
{
    public class StringLengthRule : ValidationRule
    {

        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is not string text)
                return new ValidationResult(false, "Value must be a string");

            if (MinLength != null && text.Length < MinLength)
                return new ValidationResult(false, $"Value must be at least {MinLength} charactres long");

            if (MaxLength != null && text.Length > MaxLength)
                return new ValidationResult(false, $"Value must be at most {MaxLength} characters long");

            return ValidationResult.ValidResult;
        }
    }
}
