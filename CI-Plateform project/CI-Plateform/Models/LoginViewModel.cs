using CI_Plateform.DbModels;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace CI_Plateform.Models
{
    public class LoginViewModel
    {
        public List<Banner> banner { get; set; }
        public User? User { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
