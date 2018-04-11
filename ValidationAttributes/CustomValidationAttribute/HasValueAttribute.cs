using System.Linq;
using ValueType = ValidationAttributes.CustomValidationAttribute.Values.ValueType;

namespace ValidationAttributes.CustomValidationAttribute
{
    public class HasValueAttribute : ValidationAttribute
    {
        public string[] ValidValues { get; set; }

        public override bool IsValid(object value)
        {
            return ValidValues.Contains(value?.ToString());
        }
    }
}
