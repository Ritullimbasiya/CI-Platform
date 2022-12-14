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
        public Banner? Banner { get; set; } = null!;
        public MissionMedium? missionMedium { get; set; } = null!;
        public MissionDocument? missionDocument { get; set; } = null!;
        public List<string>? addSkill { get; set; }
        [Display(Name = "Date Created")]
        public DateTime dateCreated
        {
            get { return DateTime.Now; }
        }
    }

}
