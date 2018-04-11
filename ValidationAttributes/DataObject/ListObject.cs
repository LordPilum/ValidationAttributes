using System.Collections.Generic;
using ValidationAttributes.CustomValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class ListObject
    {
        public int Id { get; set; }
        [HasValue(ValidValues = new [] { "Moana", "Mark", "Jake", "Sara" })]
        public string Name { get; set; }
        public List<ListItemObject> Items { get; set; }
    }
}
