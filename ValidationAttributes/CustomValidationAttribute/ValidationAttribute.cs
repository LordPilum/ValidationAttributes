using System;

namespace ValidationAttributes.CustomValidationAttribute
{
    public abstract class ValidationAttribute : Attribute
    {
        public abstract bool IsValid(object value);
    }
}
