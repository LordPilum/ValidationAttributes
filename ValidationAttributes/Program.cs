using System;
using System.Collections.Generic;
using ValidationAttributes.DataObject;

namespace ValidationAttributes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var obj = new RootLevel
            {
                Id = "1",
                MyObject = new SimpleObject
                {
                    Id = 5,
                    User = "Bogga"
                },
                ListOfStuff = new ListObject
                {
                    Id = 5,
                    Items = new List<ListItemObject>
                    {
                        new ListItemObject
                        {
                            Id = 4,
                            Name = "Daniel",
                            Date = DateTime.Now
                        }
                    }
                }
            };

            /*Console.WriteLine("Validating.");
            Validate(obj);
            Console.WriteLine("Done validating.");*/

            Console.WriteLine("Custom validation.");
            CustomValidate(obj);
            Console.WriteLine("Custom validation done.");

            Console.ReadKey();
        }

        private static void CustomValidate(object obj)
        {
            var errors = new List<CustomValidationAttribute.ValidationError>();
            var isValid = CustomValidationAttribute.Validator.Validate(obj, ref errors);

            if (!isValid)
            {
                foreach (var validationResult in errors)
                {
                    Console.WriteLine(validationResult.Field);
                }
            }
        }
    }
}
