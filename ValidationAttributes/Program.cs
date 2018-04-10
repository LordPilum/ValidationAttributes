using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    Id = 4
                },
                ListOfStuff = new ListObject
                {
                    Id = 5,
                    Items = new List<ListItemObject>
                    {
                        new ListItemObject
                        {
                            Id = 4,
                            Name = "Daniel"
                        }
                    }
                }
            };

            Console.WriteLine("Validating.");
            Validate(obj);
            Console.WriteLine("Done validating.");
            Console.ReadKey();
        }

        private static void Validate(object obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, context, results, true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
            }
        }
    }
}
