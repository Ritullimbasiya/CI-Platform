using CI_Plateform.DbModels;

namespace CI_Plateform.Models
{
    public class LoginViewModel
    {
        public List<Banner> banner { get; set; }
        public User? User { get; set; } = null!;

    }
}
