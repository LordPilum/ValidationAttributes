using System;
using ValidationAttributes.CustomValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class ListItemObject
    {
        [HasValue(ValidValues = new [] { "1", "4", "7" })]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        [HasValueIf(FieldName = "Id", FieldValue = "4", ValidValues = new[] { "2073-05-31T23:00:00Z" })]
        public DateTime Date { get; set; }
        public SimpleObject Data { get; set; }
    }
}
