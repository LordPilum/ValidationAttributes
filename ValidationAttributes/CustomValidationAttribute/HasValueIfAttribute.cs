using System.Linq;

namespace ValidationAttributes.CustomValidationAttribute
{
    public class HasValueIfAttribute : ValidationAttribute
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string[] ValidValues { get; set; }

        public override bool IsValid(object value)
        {
            return ValidValues.Contains(value?.ToString());
        }
    }
}
