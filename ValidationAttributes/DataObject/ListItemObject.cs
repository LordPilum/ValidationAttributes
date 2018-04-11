using ValidationAttributes.CustomValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class ListItemObject
    {
        [HasValue(ValidValues = new [] { "1", "4", "7" })]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public SimpleObject Data { get; set; }
    }
}
