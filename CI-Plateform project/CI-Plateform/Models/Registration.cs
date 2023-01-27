using CI_Plateform.DbModels;
using System.ComponentModel.DataAnnotations;

namespace CI_Plateform.Models
{
    public class Registration
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])).+$", ErrorMessage = "Password Must Contain 1-Symbol ,1-lowercase,1-Uppercase,1-digit")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public int PhoneNumber { get; set; }
        public List<Banner> banner { get; set; }
        public string Token { get; set; } = null!;
    }
}
