using System;
using System.Linq;

namespace ValidationAttributes.CustomValidationAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class HasValueIfAttribute : ValidationAttribute
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
