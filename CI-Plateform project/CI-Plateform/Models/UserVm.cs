using CI_Plateform.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CI_Plateform.Models
{
    public class UserVm
    {
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> SkillList { get; set; }
        public IEnumerable<SelectListItem> ThemeList { get; set; }
        public User? User { get; set; } = null!;
        public GoalMission? GoalMission { get; set; } = null!;
        public CmsPage? CmsPage { get; set; } = null!;
        public Mission? Mission { get; set; } = null!;
        public MissionTheme? MissionTheme { get; set; } = null!;
        public Skill? Skill { get; set; } = null!;
        public List<UserSkill>? skl { get; set; } = null!;
        public List<string>? skll { get; set; } = null!;
        public Banner? Banner { get; set; } = null!;
        public MissionMedium? missionMedium { get; set; } = null!;
        public MissionDocument? missionDocument { get; set; } = null!;
        public List<string>? addSkill { get; set; }
        public Admin? admin { get; set; }
        public ContactUs? contactUs { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])).+$", ErrorMessage = "Password Must Contain 1-Symbol ,1-lowercase,1-Uppercase,1-digit")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string newPassword { get; set; }

        public List<SelectListItem> skills { get; set; }
        public List<SelectListItem>? userOldSkill { get; set; }
        public List<string>? userNewSkill { get; set; }


        [Display(Name = "Date Created")]
        public DateTime dateCreated
        {
            get { return DateTime.Now; }
        }
    }

}
