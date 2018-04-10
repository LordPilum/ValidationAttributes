using System.ComponentModel.DataAnnotations;
using ValidationAttributes.ValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class RootLevel
    {
        [Required]
        public string Id { get; set; }
        [RequiredIf("Id", 1)]
        public string ResponsibleAgency { get; set; }
        [Required]
        public int CodeListId { get; set; }
        public SimpleObject MyObject { get; set; }
        public ListObject ListOfStuff { get; set; }
    }
}
