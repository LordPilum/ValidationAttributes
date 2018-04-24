using System;
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
        /// <param name="obj">Object to be validated.</param>
        /// <param name="errors">ref list of ValidationError.</param>
        /// <returns>boolean isValid</returns>
        public static bool Validate(object obj, ref List<ValidationError> errors)
        {
            return Validate(obj, null, null, ref errors);
        }

        /// <summary>
        /// Recursive validation method.
        /// </summary>
        /// <param name="rootObj">The root object to be validated.</param>
        /// <param name="obj">Object to be validated. This might be a descendant of the root object.</param>
        /// <param name="parentPath">Breadcrumb path for tracking.</param>
        /// <param name="errors">ref list of ValidationError.</param>
        /// <returns></returns>
        private static bool Validate(object rootObj, object obj, string parentPath, ref List<ValidationError> errors)
        {
            if (rootObj == null)
                return false;

            if (obj == null)
                obj = rootObj;

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
                            isValid &= Validate(rootObj, pV, string.Concat(path, $"[{i++}]"), ref errors);
                    }
                    else
                        isValid &= Validate(rootObj, propVal, path, ref errors);

                    continue;
                }

                var attrs = prop.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (!(attr is ValidationAttribute)) continue;

                    if (attr is HasValueAttribute valAttr)
                        isValid &= ValidateHasValueAttribute(rootObj, obj, parentPath, prop, valAttr, ref errors);

                    if (attr is HasValueIfAttribute valIfAttr)
                        isValid &= ValidateHasValueIfAttribute(rootObj, obj, parentPath, prop, valIfAttr, ref errors);
                }
            }

            return isValid;
        }

        /// <summary>
        /// Validation has value attribute.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parentPath"></param>
        /// <param name="prop"></param>
        /// <param name="attr"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool ValidateHasValueAttribute(object obj, string parentPath, PropertyInfo prop, HasValueAttribute attr, ref List<ValidationError> errors)
        {
            var isValid = true;
            var propVal = GetPropValue(obj, prop.Name);
            if (!attr.IsValid(propVal))
            {
                isValid = false;
                var path = string.Concat(parentPath, ".", prop.Name);
                errors.Add(new ValidationError(ValidationErrorType.IsEmpty, path));
            }

            return isValid;
        }

        private static bool ValidateHasValueIfAttribute(object rootObj, object obj, string parentPath, PropertyInfo prop, HasValueIfAttribute attr, ref List<ValidationError> errors)
        {
            var isValid = true;
            var propVal = GetPropValue(obj, prop.Name);
            var dependantPropVal = GetPropValue(rootObj, obj, attr.FieldName);

            if (dependantPropVal == null)
                return true;

            if(dependantPropVal.ToString().Equals(attr.FieldValue))
                if (!attr.IsValid(propVal))
                {
                    isValid = false;
                    var path = string.Concat(parentPath, ".", prop.Name);
                    errors.Add(new ValidationError(ValidationErrorType.IsEmpty, path));
                }

            return isValid;
        }


        /// <summary>
        /// Gets the value of a property by property name.
        /// </summary>
        /// <param name="src">Source object.</param>
        /// <param name="propName">Property name.</param>
        /// <returns></returns>
        public static object GetPropValue(object src, string propName)
        {
            while (true)
            {
                if (src == null) throw new ArgumentException("Value cannot be null.", nameof(src));
                if (propName == null) throw new ArgumentException("Value cannot be null.", nameof(propName));

                var prop = src.GetType().GetProperty(propName);
                return prop?.GetValue(src, null);
            }
        }


        /// <summary>
        /// Gets the value of a property by property name.
        /// </summary>
        /// <param name="rootObj">Root object.</param>
        /// <param name="src">Source object.</param>
        /// <param name="propName">Property name.</param>
        /// <returns></returns>
        public static object GetPropValue(object rootObj, object src, string propName)
        {
            while (true)
            {
                if (rootObj == null) throw new ArgumentException("Value cannot be null.", nameof(rootObj));
                if (propName == null) throw new ArgumentException("Value cannot be null.", nameof(propName));

                if (propName.Contains("."))
                {
                    var temp = propName.Split(new [] {'.'}, 2);
                    src = GetPropValue(rootObj, src, temp[0]);
                    propName = temp[1];
                    continue;
                }

                var obj = src ?? rootObj;

                var prop = obj.GetType().GetProperty(propName);
                return prop?.GetValue(obj, null);
            }
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
    }
}
