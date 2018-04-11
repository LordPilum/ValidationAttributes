using ValidationAttributes.CustomValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class RootLevel
    {
        public string Id { get; set; }
        [HasValue(ValidValues = new [] { "9", "89", "206" })]
        public string ResponsibleAgency { get; set; }
        [HasValueIf(FieldName = "ResponsibleAgency", FieldValue = "206", ValidValues = new [] { "SVK" })]
        public int CodeListId { get; set; }
        public SimpleObject MyObject { get; set; }
        public ListObject ListOfStuff { get; set; }
    }
}
