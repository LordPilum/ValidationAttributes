using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ValidationAttributes.CustomValidationAttribute
{
    public class Validator
    {
        /// <summary>
        /// Validator entry point.
        /// </summary>
        /// <param name="obj">Object to be validated</param>
        /// <param name="errors">ref list of ValidationError</param>
        /// <returns>boolean isValid</returns>
        public static bool Validate(object obj, ref List<ValidationError> errors)
        {
            return Validate(obj, null, ref errors);
        }

        private static bool Validate(object obj, string parentPath, ref List<ValidationError> errors)
        {
            if (obj == null)
                return false;

            var isValid = true;

            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                var isEnoroClass = prop.PropertyType.FullName?.Contains("DataObject") ?? false;

                if (isEnoroClass)
                {
                    var path = string.Concat(parentPath, parentPath == null ? "" : ".", prop.Name);

                    var propVal = GetPropValue(obj, prop.Name);
                    if (propVal == null)
                        continue;

                    if (IsEnumerable(prop))
                    {
                        var i = 0;
                        foreach (var pV in (IEnumerable) propVal)
                            isValid &= Validate(pV, string.Concat(path, $"[{i++}]"), ref errors);
                    }
                    else
                        isValid &= Validate(propVal, path, ref errors);

                    continue;
                }

                var attrs = prop.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (!(attr is ValidationAttribute authAttr)) continue;

                    var propVal = GetPropValue(obj, prop.Name);
                    if (!authAttr.IsValid(propVal))
                    {
                        isValid = false;
                        var path = string.Concat(parentPath, ".", prop.Name);
                        errors.Add(new ValidationError(ValidationErrorType.IsEmpty, path));
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Gets the value of a property by property name.
        /// </summary>
        /// <param name="src">Source object.</param>
        /// <param name="propName">Property name.</param>
        /// <returns></returns>
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }

        /// <summary>
        /// Checks if a property is or inherits from IEnumerable.
        /// </summary>
        /// <param name="pi">PropertyInfo of the property.</param>
        /// <returns></returns>
        private static bool IsEnumerable(PropertyInfo pi)
        {
            return typeof(IEnumerable).IsAssignableFrom(pi.PropertyType);
        }

        /// <summary>
        /// Validating the actual content.
        /// </summary>
        /// <param name="value">Property value.</param>
        /// <param name="name">Name of the property.</param>
        /// <param name="picture">Validation picture.</param>
        /// <param name="errors">A list of validation errors, by ref.</param>
        private static void ValidateFormat(object value, string name, string picture, ref List<ValidationError> errors)
        {
            if (value == null)
                return;

            if (picture.StartsWith("X"))
            {
                /*if (value.GetType().IsAssignableFrom(typeof(int)))
                {
                    errors.Add(new EdiError(EdiErrorType.FormatError, name));
                    return;
                }*/

                if (!value.GetType().IsAssignableFrom(typeof(string)))
                    return;

                var lengthStr = picture.Substring(2, picture.Length - 3);

                if (!int.TryParse(lengthStr, out var length)) return;

                if (((string)value).Length > length)
                    errors.Add(new ValidationError(ValidationErrorType.IsTooLong, name));
            }
            else if (picture.StartsWith("9"))
            {
                if (!(value.GetType().IsAssignableFrom(typeof(int))
                      || !value.GetType().IsAssignableFrom(typeof(decimal)))) return;

                var lengthStr = picture.Substring(2, picture.Length - 3);
                if (!int.TryParse(lengthStr, out var length)) return;

                if (value.ToString().Length > length)
                    errors.Add(new ValidationError(ValidationErrorType.IsTooLong, name));
            }
        }
    }
}
