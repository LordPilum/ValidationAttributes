using ValidationAttributes.CustomValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class SimpleObject
    {
        public int Id { get; set; }
        [HasValueIf(FieldName = "MyObject.Id", FieldValue = "5",  ValidValues = new [] {"Sola", "Bogga"})]
        [HasValueIf(FieldName = "MyObject.Id", FieldValue = "7",  ValidValues = new [] {"Sander", "Molex"})]
        public string User { get; set; }
        public string Notice { get; set; }
    }
}
