using System.ComponentModel.DataAnnotations;
using ValidationAttributes.CustomValidationAttribute;
using ValidationAttributes.ValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class ListItemObject
    {
        [Required]
        [HasValue(ValidValues = new [] { "1", "4", "7" })]
        public int Id { get; set; }
        public string Name { get; set; }
        [RequiredIf("Name", "Daniel")]
        public decimal Value { get; set; }
        public SimpleObject Data { get; set; }
    }
}
