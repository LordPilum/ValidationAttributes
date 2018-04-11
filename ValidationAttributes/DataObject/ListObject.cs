using System.Collections.Generic;
using Microsoft.Build.Framework;
using ValidationAttributes.CustomValidationAttribute;
using ValidationAttributes.ValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class ListObject
    {
        [Required]
        public int Id { get; set; }
        [RequiredIf("Id", 5)]
        [HasValue(ValidValues = new [] { "Moana", "Mark", "Jake", "Sara" })]
        public string Name { get; set; }
        public List<ListItemObject> Items { get; set; }
    }
}
