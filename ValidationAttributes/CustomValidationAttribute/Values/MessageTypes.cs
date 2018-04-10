using System.Collections.Generic;
using System.Linq;

namespace ValidationAttributes.CustomValidationAttribute.Values
{
    public class MessageTypes : ValueType
    {
        private static readonly IReadOnlyList<string> Values = new List<string>
        {
            "E11",
            "E36",
            "E66",
            "Z02",
            "Z07"
        };

        public override bool Contains(object value)
        {
            return Values.Contains(value);
        }
    }
}
