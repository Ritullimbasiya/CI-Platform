using CI_Plateform.DbModels;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace CI_Plateform.Models
{
    public class LoginViewModel
    {
        public List<Banner> banner { get; set; }
        public User? User { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])).+$", ErrorMessage = "Password Must Contain 1-Symbol ,1-lowercase,1-Uppercase,1-digit")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
    }
}
