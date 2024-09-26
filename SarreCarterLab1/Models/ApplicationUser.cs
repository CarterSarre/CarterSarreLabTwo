using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First name is required!")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}
