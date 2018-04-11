using ValidationAttributes.CustomValidationAttribute;

namespace ValidationAttributes.DataObject
{
    public class SimpleObject
    {
        public int Id;
        [HasValueIf(FieldName = "MyObject.Id", FieldValue = "5",  ValidValues = new [] {"Sola", "Bogga"})]
        public string User { get; set; }
        public string Notice { get; set; }
    }
}
