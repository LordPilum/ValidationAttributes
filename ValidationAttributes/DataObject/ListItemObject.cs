using System.ComponentModel.DataAnnotations;
using ValidationAttributes.ValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class ListItemObject
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [RequiredIf("Name", "Daniel")]
        public decimal Value { get; set; }
        public SimpleObject Data { get; set; }
    }
}
