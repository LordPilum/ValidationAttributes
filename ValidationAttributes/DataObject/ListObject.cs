using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Build.Framework;
using ValidationAttributes.ValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class ListObject
    {
        [Required]
        public int Id { get; set; }
        [RequiredIf("Id", 5)]
        public string Name { get; set; }
        public List<ListItemObject> Items { get; set; }
    }
}
