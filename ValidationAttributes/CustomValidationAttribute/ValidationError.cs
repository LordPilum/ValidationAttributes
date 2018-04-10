namespace ValidationAttributes.CustomValidationAttribute
{
    public class ValidationError
    {
        public ValidationError(ValidationErrorType errorType, string field)
        {
            Field = field;
            ErrorType = errorType;
        }

        public ValidationErrorType ErrorType { get; set; }
        public string Field { get; set; }
    }

    public enum ValidationErrorType
    {
        IsEmpty,
        IsNotEmpty,
        IsTooLong,
        DateIsTooHigh,
        DateIsTooLow,
        WrongFormat
    }
}
