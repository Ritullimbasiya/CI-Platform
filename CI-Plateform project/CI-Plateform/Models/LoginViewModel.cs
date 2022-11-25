using CI_Plateform.DbModels;

namespace CI_Plateform.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<Banner> banner { get; set; } = null!;

    }
}
