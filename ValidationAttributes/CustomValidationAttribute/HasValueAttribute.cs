using System;
using System.Linq;

namespace ValidationAttributes.CustomValidationAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class HasValueAttribute : ValidationAttribute
    {
        public string[] ValidValues { get; set; }

        public override bool IsValid(object value)
        {
            return ValidValues.Contains(value?.ToString());
        }
    }
}
