using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Employer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Phone Number is required!")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Website is required!")]
        [Display(Name = "Website URL")]
        [DataType(DataType.Url)]
        public string? Website { get; set; }

        [Display(Name = "Incorporated Date")]
        [DataType(DataType.Date)]
        public DateTime? IncorporatedDate { get; set; }
    }
}