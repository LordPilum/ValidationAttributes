using System.Linq;

namespace ValidationAttributes.CustomValidationAttribute.Values
{
    public class CodeListResponsibles
    {
        public static readonly string[] Values = new []
        {
            "9",
            "89",
            "206"
        };

        public static bool Contains(object value)
        {
            return Values.Contains(value);
        }
    }
}
