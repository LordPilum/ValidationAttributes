using System.ComponentModel.DataAnnotations;
using ValidationAttributes.CustomValidationAttribute;
using ValidationAttributes.CustomValidationAttribute.Values;
using ValidationAttributes.ValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class RootLevel
    {
        [Required]
        public string Id { get; set; }
        [RequiredIf("Id", 1)]
        [HasValue(ValidValues = new [] {"9","89","206"})]
        public string ResponsibleAgency { get; set; }
        [Required]
        public int CodeListId { get; set; }
        public SimpleObject MyObject { get; set; }
        public ListObject ListOfStuff { get; set; }
    }
}
