using System.ComponentModel.DataAnnotations;

namespace ValidationAttributes.DataObject
{
    public class SimpleObject
    {
        [Required]
        public int Id;
        [Required]
        public string User { get; set; }
        public string Notice { get; set; }
    }
}
