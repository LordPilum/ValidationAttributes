using System.Collections.Generic;
using System.Linq;
using ValueType = ValidationAttributes.CustomValidationAttribute.Values.ValueType;

namespace ValidationAttributes.CustomValidationAttribute
{
    public class HasValueAttribute : ValidationAttribute
    {
        public object Field { get; set; }
        public string[] ValidValues { get; set; }

        public override bool IsValid()
        {
            return ValidValues.Contains("_value");
        }
    }
}
